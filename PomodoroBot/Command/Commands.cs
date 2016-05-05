using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroBot.Command
{
    public static class Commands
    {
        public static IEnumerable<ICommand> GetItems()
        {
            yield return new CreateCommand();
            yield return new StartCommand();
            yield return new StopCommand();
            yield return new DeleteCommand();
            yield return new ListCommand();
        }
    }
}
