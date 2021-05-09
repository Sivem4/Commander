using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.DTOs;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        // GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
        }

        // GET api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);
            
            if(command != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(command));
            }
            return NotFound();
        }

        // POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDTO);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadDTO = _mapper.Map<CommandReadDTO>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDTO.Id}, commandReadDTO);
        }
        
        // PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDTO commandUpdateDTO)
        {
            var commandModel = _repository.GetCommandById(id);
            if(commandModel == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDTO, commandModel);
            _repository.UpdateCommand(commandModel);
            _repository.SaveChanges();
            return NoContent();
        }

        // PATCH api/cpmmands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateCommand(int id, JsonPatchDocument<CommandUpdateDTO> patchDoc)
        {
            var commandModel = _repository.GetCommandById(id);
            if(commandModel == null)
            {
                return NotFound();
            }
            var commandToPatch = _mapper.Map<CommandUpdateDTO>(commandModel);
            patchDoc.ApplyTo(commandToPatch, ModelState);
            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModel);
            _repository.UpdateCommand(commandModel);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModel = _repository.GetCommandById(id);
            if(commandModel == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandModel);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}