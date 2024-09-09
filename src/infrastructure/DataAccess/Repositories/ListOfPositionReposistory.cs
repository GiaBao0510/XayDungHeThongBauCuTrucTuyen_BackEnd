using BackEnd.core.Entities;
using BackEnd.src.infrastructure.DataAccess.Context;
using BackEnd.src.infrastructure.DataAccess.IRepository;
using MySql.Data.MySqlClient;

namespace BackEnd.src.infrastructure.DataAccess.Repositories
{
    public class ListOfPositionReposistory : IDisposable,IListOfPositionRepository
    {
        private readonly DatabaseContext _context;

        //Khởi tạo
        public ListOfPositionReposistory(DatabaseContext context) => _context = context;

        //hủy
        public void Dispose() => _context.Dispose();

        //Liệt kê
        public async Task<List<ListOfPositions>> _GetListOfListOfPositions(){
            var list = new List<ListOfPositions>();

            using var connection = await _context.Get_MySqlConnection();
            using var command = new MySqlCommand("SELECT * FROM danhmucungcu", connection);
            using var reader = await command.ExecuteReaderAsync();
            
            while(await reader.ReadAsync()){
                list.Add(new ListOfPositions{
                    ID_Cap = reader.GetInt32(reader.GetOrdinal("ID_Cap")),
                    TenCapUngCu = reader.GetString(reader.GetOrdinal("TenCapUngCu")),
                    ID_DonViBauCu = reader.GetInt32(reader.GetOrdinal("ID_DonViBauCu"))
                });
            }
            return list;
        }

        //thêm
        public async Task<bool> _AddListOfPositions(ListOfPositions danhmucungcu){
            using var connection = await _context.Get_MySqlConnection();
            
            //Kiểm tra mã số đơn vị bầu cử tại danhmucungcu có tồn tại không
            string checkInput = "SELECT COUNT(*) FROM donvibaucu WHERE ID_DonViBauCu = @ID_DonViBauCu";
            using(var commandCheck = new MySqlCommand(checkInput, connection)){
                commandCheck.Parameters.AddWithValue("@ID_DonViBauCu",danhmucungcu.ID_DonViBauCu);
                int count = Convert.ToInt32(await commandCheck.ExecuteScalarAsync());
                if(count < 1)
                    return false;
            }

            //Thực hiện thêm            
            string Input = $"INSERT INTO danhmucungcu(TenCapUngCu,ID_DonViBauCu) VALUES(@TenCapUngCu,@ID_DonViBauCu);";
            using (var commandAdd = new MySqlCommand(Input, connection)){
                commandAdd.Parameters.AddWithValue("@TenCapUngCu",danhmucungcu.TenCapUngCu);
                commandAdd.Parameters.AddWithValue("@ID_DonViBauCu",danhmucungcu.ID_DonViBauCu);
                await commandAdd.ExecuteNonQueryAsync();
            } 

            return true; 
        }

        //Lấy theo ID
        public async Task<ListOfPositions> _GetListOfPositionsBy_ID(string id){
            using var connection = await _context.Get_MySqlConnection();

            const string sql = @"
                SELECT * FROM danhmucungcu 
                WHERE ID_Cap = @ID_Cap";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID_Cap",id);

            using var reader = await command.ExecuteReaderAsync();
            if(await reader.ReadAsync()){
                return new ListOfPositions{
                    ID_Cap = reader.GetInt32(reader.GetOrdinal("ID_Cap")),
                    TenCapUngCu = reader.GetString(reader.GetOrdinal("TenCapUngCu")),
                    ID_DonViBauCu = reader.GetInt32(reader.GetOrdinal("ID_DonViBauCu"))
                };
            }

            return null;
        }

        //Sửa
        public async Task<bool> _EditListOfPositionsBy_ID(string ID, ListOfPositions ListOfPositions){
            using var connection = await _context.Get_MySqlConnection();

            //Tìm kiếm quận huyện có tồn tại không
            const string sqlCheck = "SELECT COUNT(*) FROM donvibaucu WHERE ID_DonViBauCu = @ID_DonViBauCu";
            using(var command0 = new MySqlCommand(sqlCheck, connection)){
                command0.Parameters.AddWithValue("@ID_DonViBauCu",ListOfPositions.ID_DonViBauCu);
                int count = Convert.ToInt32(await command0.ExecuteScalarAsync());
                
                if(count < 1)
                    return false;
            }

            //Cập nhật
            const string sqlupdate = @"UPDATE danhmucungcu SET TenCapUngCu = @TenCapUngCu, ID_DonViBauCu = @ID_DonViBauCu WHERE ID_Cap = @ID_Cap";
            using( var command = new MySqlCommand(sqlupdate, connection)){
                command.Parameters.AddWithValue("@ID_Cap",ID);
                command.Parameters.AddWithValue("@TenCapUngCu",ListOfPositions.TenCapUngCu);
                command.Parameters.AddWithValue("@ID_DonViBauCu",ListOfPositions.ID_DonViBauCu);

                //Lấy số hàng bị tác động nếu > 0 thì true, ngược lại là false
                int rowAffected = await command.ExecuteNonQueryAsync();
                return rowAffected > 0;
            }
            
        }

        //Xóa
        public async Task<bool> _DeleteListOfPositionsBy_ID(string ID){
            using var connection = await _context.Get_MySqlConnection();

            const string sqlupdate = @"
                DELETE FROM danhmucungcu
                WHERE ID_Cap = @ID_Cap";
            
            using var command = new MySqlCommand(sqlupdate, connection);
            command.Parameters.AddWithValue("@ID_Cap",ID);
        
            //Lấy số hàng bị tác động nếu > 0 thì true, ngược lại là false
            int rowAffected = await command.ExecuteNonQueryAsync();
            return rowAffected > 0;
        }

    }
}