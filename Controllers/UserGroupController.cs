using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vize.Dtos;
using vize.Models;

namespace vize.Controllers
{
    [Route("api/UserGroup")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public UserGroupController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<UserGroupDto> GetList()
        {
            var UserGroups = _context.UserGroups.ToList();
            var UserGroupDtos = _mapper.Map<List<UserGroupDto>>(UserGroups);
            return UserGroupDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public UserGroupDto Get(int id)
        {
            var UserGroup = _context.UserGroups.Where(s => s.Id == id).SingleOrDefault();
            var UserGroupDto = _mapper.Map<UserGroupDto>(UserGroup);
            return UserGroupDto;
        }

        [HttpPost]
        public ResultDto Post(UserGroupDto dto)
        {
            if (_context.UserGroups.Count(c => c.Name == dto.Name) > 0)
            {
                result.Status = false;
                result.Message = "Grup Kayıtlıdır!";
                return result;
            }
            var UserGroup = _mapper.Map<UserGroup>(dto);
            UserGroup.Updated = DateTime.Now;
            UserGroup.Created = DateTime.Now;
            _context.UserGroups.Add(UserGroup);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Grup Eklendi";
            return result;
        }


        [HttpPut]
        public ResultDto Put(UserGroupDto dto)
        {
            var UserGroup = _context.UserGroups.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (UserGroup == null)
            {
                result.Status = false;
                result.Message = "Grup Bulunamadı!";
                return result;
            }
            UserGroup.Name = dto.Name;
            UserGroup.UyeSayısı = dto.UyeSayısı;
            UserGroup.Updated = DateTime.Now;
            _context.UserGroups.Update(UserGroup);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Grup Düzenlendi";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        public ResultDto Delete(int id)
        {
            var UserGroup = _context.UserGroups.Where(s => s.Id == id).SingleOrDefault();
            if (UserGroup == null)
            {
                result.Status = false;
                result.Message = "Grup Bulunamadı!";
                return result;
            }
            _context.UserGroups.Remove(UserGroup);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Grup Silindi";
            return result;
        }
    }
}
