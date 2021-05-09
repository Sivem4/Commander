using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>()
            {
                new Command(){Id = 0, HowTo = "Boil an egg",  Line = "Boil water", Platform = "Kettle and pan"},
                new Command(){Id = 1, HowTo = "Cut bread",  Line = "Cutting knife", Platform = "Knife and chopping board"},
                new Command(){Id = 2, HowTo = "Make cup of tea",  Line = "Put teabag in cup", Platform = "Kettle and cup"}
            };

            return commands;
        }

        public Command GetCommandById(int id) => 
        new Command(){Id = 0, HowTo = "Boil an egg",  Line = "Boil water", Platform = "Kettle and pan"};

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}