using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vize.Dtos;
using vize.Models;

namespace vize.Controllers
{
    [Route("api/Klasors[action]")]
    [ApiController]
    public class KlasorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public KlasorController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<KlasorDto> GetList()
        {
            var klasor = _context.Klasors.ToList();
            var KlasorDtos = _mapper.Map<List<KlasorDto>>(klasor);
            return KlasorDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public KlasorDto Get(int id)
        {
            var klasor = _context.Klasors.Where(s => s.Id == id).SingleOrDefault();
            var KlasorDto = _mapper.Map<KlasorDto>(klasor);
            return KlasorDto;
        }

        [HttpPost]
        public ResultDto Post(KlasorDto dto)
        {
            if (_context.Klasors.Count(c => c.Name == dto.Name) > 0)
            {
                result.Status = false;
                result.Message = "Girilen Ürün Adı Kayıtlıdır!";
                return result;
            }
            var klasor = _mapper.Map<Klasor>(dto);
            klasor.Updated = DateTime.Now;
            klasor.Created = DateTime.Now;
            _context.Klasors.Add(klasor);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Ürün Eklendi";
            return result;
        }


        [HttpPut]
        public ResultDto Put(KlasorDto dto)
        {
            var klasor = _context.Klasors.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (klasor == null)
            {
                result.Status = false;
                result.Message = "Ürün Bulunamadı!";
                return result;
            }
            klasor.Name = dto.Name;
            klasor.IsActive = dto.IsActive;
            klasor.Updated = DateTime.Now;
            klasor.Description = dto.Description;
            _context.Klasors.Update(klasor);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Ürün Düzenlendi";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        public ResultDto Delete(int id)
        {
            var klasor = _context.Klasors.Where(s => s.Id == id).SingleOrDefault();
            if (klasor == null)
            {
                result.Status = false;
                result.Message = "Ürün Bulunamadı!";
                return result;
            }
            _context.Klasors.Remove(klasor);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Ürün Silindi";
            return result;
        }
    }
}

