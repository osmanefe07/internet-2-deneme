using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vize.Dtos;
using vize.Models;

namespace vize.Controllers
{
    [Route("api/Dosyas[action]")]
    [ApiController]
    public class DosyaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public DosyaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<DosyaDto> GetList()
        {
            var dosya = _context.Dosyas.ToList();
            var DosyaDtos = _mapper.Map<List<DosyaDto>>(dosya);
            return DosyaDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public DosyaDto Get(int id)
        {
            var dosya = _context.Dosyas.Where(s => s.Id == id).SingleOrDefault();
            var DosyaDto = _mapper.Map<DosyaDto>(dosya);
            return DosyaDto;
        }

        [HttpPost]
        public ResultDto Post(DosyaDto dto)
        {
            if (_context.Dosyas.Count(c => c.Name == dto.Name) > 0)
            {
                result.Status = false;
                result.Message = "Girilen Dosya Kayıtlıdır!";
                return result;
            }
            var dosya = _mapper.Map<Dosya>(dto);
            dosya.Updated = DateTime.Now;
            dosya.Created = DateTime.Now;
            _context.Dosyas.Add(dosya);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Dosya Eklendi";
            return result;
        }


        [HttpPut]
        public ResultDto Put(DosyaDto dto)
        {
            var dosya = _context.Dosyas.Where(s => s.Id == dto.Id).SingleOrDefault();
            if (dosya == null)
            {
                result.Status = false;
                result.Message = "Dosya Bulunamadı!";
                return result;
            }
            dosya.Name = dto.Name;
            dosya.IsActive = dto.IsActive;
            dosya.Size = dto.Size;
            dosya.Updated = DateTime.Now;
            dosya.Description = dto.Description;
            _context.Dosyas.Update(dosya);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Dosya Düzenlendi";
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        public ResultDto Delete(int id)
        {
            var dosya = _context.Dosyas.Where(s => s.Id == id).SingleOrDefault();
            if (dosya == null)
            {
                result.Status = false;
                result.Message = "Dosya Bulunamadı!";
                return result;
            }
            _context.Dosyas.Remove(dosya);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Dosya Silindi";
            return result;
        }
    }
}
