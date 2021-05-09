using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _commanderContext;

        public SqlCommanderRepo(CommanderContext commanderContext)
        {
            _commanderContext = commanderContext;
        }
        public IEnumerable<Command> GetAllCommands()
        {
            return _commanderContext.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _commanderContext.Commands.FirstOrDefault(c => c.Id == id);
        }
        public bool SaveChanges()
        {
            return (_commanderContext.SaveChanges() > 0);
        }

        public void CreateCommand(Command cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _commanderContext.Add(cmd);
        }
        public void UpdateCommand(Command cmd)
        {
            //Nothing
        }
        
        public void DeleteCommand(Command cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _commanderContext.Remove(cmd);
        }
    }
}