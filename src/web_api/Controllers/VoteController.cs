using BackEnd.src.infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using BackEnd.core.Entities;
using BackEnd.src.web_api.DTOs;

namespace BackEnd.src.web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly VoteReposistory _voteReposistory;

        //Khởi tạo
        public VoteController(VoteReposistory voteReposistory) => _voteReposistory = voteReposistory;

        //Liệt kê
        [HttpGet]
        public async Task<IActionResult> GetListOfVote(){
            try{
                var result = await _voteReposistory._GetListOfVote();
                
                if(result.Count == 0)
                    return StatusCode(200, new{
                        Status = "Ok",
                        Message = "Danh sách rỗng",
                    });
                
                return Ok(new{
                    Status = "Ok",
                    Message = "null",
                    Data = result
                });
            }catch(Exception ex){
                Console.WriteLine($"Erro message: {ex.Message}");
                Console.WriteLine($"Erro message: {ex.Source}");
                Console.WriteLine($"Erro InnerException: {ex.InnerException}");
                return StatusCode(500,new{
                    Status = "false",
                    Message=$"Lỗi khi truy xuất danh sách các phiếu bầu: {ex.Message}"
                });
            }
        }

        //Thêm phiếu bởi số lượng
        [HttpPost]
        [Route("add-vote-by-number")]
        public async Task<IActionResult> CreateVoteByNumber([FromQuery] int number,[FromBody] VoteDto Vote){
            try{
                //Kiểm tra number
                if(number < 0)
                    return StatusCode(400,new{
                        Status = "false",
                        Message=$"Lỗi số lượng thêm vào không được âm"
                    });

                //Kiểm tra đầu vào
                if(Vote == null || string.IsNullOrEmpty(Vote.ngayBD.ToString()))
                    return StatusCode(400,new{
                        Status = "false",
                        Message=$"Lỗi khi đầu vào không được rỗng"
                    });
                
                //lấy kết quả thêm vào được hay không
                var result = await _voteReposistory._CreateVoteByNumber(number,Vote);
                if(result == false)
                    return StatusCode(400,new{
                        Status = "false",
                        Message=$"Lỗi ngayBD không tồn tại."
                    });
                
                return Ok(new{
                    Status = "OK", 
                    Message = "Thêm phiếu bầu thành công"
                });
            }catch(Exception ex){
                Console.WriteLine($"Erro message: {ex.Message}");
                Console.WriteLine($"Erro message: {ex.Source}");
                Console.WriteLine($"Erro InnerException: {ex.InnerException}");
                return StatusCode(500, new{
                    Status = "False", 
                    Message = $"Lỗi khi thực hiện thêm phiếu bầu: {ex.Message}"
                });
            }
        }
    
        //Lấy theo ID
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetVoteBy_ID(string id){
            try{
                var Vote = await _voteReposistory._GetVoteBy_ID(id);
                if(Vote == null)
                    return StatusCode(400, new{
                    Status = "False", 
                    Message = $"Lỗi ID phiếu bầu của phiếu bầu không tồn tại"
                });

                return Ok(new{
                    Status = "Ok",
                    Message = "null",
                    Data = Vote
                });
            }catch(Exception ex){
                Console.WriteLine($"Erro message: {ex.Message}");
                Console.WriteLine($"Erro message: {ex.Source}");
                Console.WriteLine($"Erro InnerException: {ex.InnerException}");
                return StatusCode(500, new{
                    Status = "False", 
                    Message = $"Lỗi khi thực hiện lấy ID phiếu bầu: {ex.Message}"
                });
            }
        }

        //Lấy theo ID
        [HttpGet("get-by-time/{time}")]
        public async Task<IActionResult> GetVoteBy_Time(string time){
            try{
                var Vote = await _voteReposistory._GetVoteBy_Time(time);
                if(Vote == null)
                    return StatusCode(400, new{
                    Status = "False", 
                    Message = $"Lỗi time phiếu bầu của phiếu bầu không tồn tại"
                });

                return Ok(new{
                    Status = "Ok",
                    Message = "null",
                    Data = Vote
                });
            }catch(Exception ex){
                Console.WriteLine($"Erro message: {ex.Message}");
                Console.WriteLine($"Erro message: {ex.Source}");
                Console.WriteLine($"Erro InnerException: {ex.InnerException}");
                return StatusCode(500, new{
                    Status = "False", 
                    Message = $"Lỗi khi thực hiện lấy ID phiếu bầu: {ex.Message}"
                });
            }
        }

        //Sửa
        [HttpPut("get-by-id/{id}")]
        public async Task<IActionResult> EditVoteBy_ID(string id, VoteDto Vote){
            try{
                if(Vote == null || string.IsNullOrEmpty(Vote.ngayBD.ToString()))
                    return StatusCode(400, new{
                        Status = "False", 
                        Message = $"Lỗi đầu vào không được để trống"
                    });

                var result = await _voteReposistory._EditVoteBy_ID(id, Vote);
                if(result == false)
                    return StatusCode(400, new{
                        Status = "False", 
                        Message = $"Lỗi đầu vào không hợp lệ"
                    });

                return Ok(new{
                    Status = "Ok",
                    Message = "Cập nhật thành công",
                    Data = Vote
                });
            }catch(Exception ex){
                Console.WriteLine($"Erro message: {ex.Message}");
                Console.WriteLine($"Erro message: {ex.Source}");
                Console.WriteLine($"Erro InnerException: {ex.InnerException}");
                return StatusCode(500, new{
                    Status = "False", 
                    Message = $"Lỗi khi thực hiện sửa phiếu bầu: {ex.Message}"
                });
            }
        }

        //xóa
        [HttpDelete("delete-by-id/{id}")]
        public async Task<IActionResult> DeleteVoteBy_ID(string id){
            try{
                var result = await _voteReposistory._DeleteVoteBy_ID(id);
                if(result == false)
                    return StatusCode(400, new{
                        Status = "False", 
                        Message = $"Lỗi ID_phiếu bầu không tồn tại"
                    });

                return Ok(new{
                    Status = "Ok",
                    Message = "Xóa thành công"
                });
            }catch(Exception ex){
                Console.WriteLine($"Erro message: {ex.Message}");
                Console.WriteLine($"Erro message: {ex.Source}");
                Console.WriteLine($"Erro InnerException: {ex.InnerException}");
                return StatusCode(500, new{
                    Status = "False", 
                    Message = $"Lỗi khi thực hiện xóa phiếu bầu: {ex.Message}"
                });
            }
        }

        //xóa dựa trên thời điểm
        [HttpDelete("delete-by-time/{thoidiem}")]
        public async Task<IActionResult> DeleteVoteBy_Time(string thoidiem){
            try{
                var result = await _voteReposistory._DeleteVoteBy_Time(thoidiem);
                if(result == false)
                    return StatusCode(400, new{
                        Status = "False", 
                        Message = $"Lỗi thời điểm phiếu bầu không tồn tại"
                    });

                return Ok(new{
                    Status = "Ok",
                    Message = "Xóa thành công"
                });
            }catch(Exception ex){
                Console.WriteLine($"Erro message: {ex.Message}");
                Console.WriteLine($"Erro message: {ex.Source}");
                Console.WriteLine($"Erro InnerException: {ex.InnerException}");
                return StatusCode(500, new{
                    Status = "False", 
                    Message = $"Lỗi khi thực hiện xóa phiếu bầu: {ex.Message}"
                });
            }
        }
    }
}