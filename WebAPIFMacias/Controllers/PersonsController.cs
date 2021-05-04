using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IPersonsRepository _personsRepository;

        public PersonsController(ILogger<PersonsController> logger, IPersonsRepository personsRepository )
        {
            _logger = logger;
            _personsRepository = personsRepository;
        }
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _personsRepository.GetAll();
        }
        [HttpGet("{id}")]
        public Person GetPerson(long id)
        {
            return _personsRepository.GetPerson(id);
        }
        [HttpGet("color/{color}")]
        public IEnumerable<Person> GetPersonsByColor(int color)
        {
            return _personsRepository.GetPersonsByColor(color);
        }
    }
}
