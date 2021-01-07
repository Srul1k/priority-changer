﻿using System;

namespace ppc.Commands
{
    class UpdateCommand : ICancelable
    {
        private string _key;
        private CpuPriorityLevel _priorityLevel;
        private CpuPriorityLevel _oldPriorityLevel;

        public UpdateCommand(string key, int priorityLevel)
        {
            if (key == null)
            {
                throw new ArgumentNullException("The key value is null");
            }

            _key = key.Contains(".") ? key : key + ".exe";

            if (priorityLevel < 1 || priorityLevel > 6)
            {
                throw new ArgumentException("Wrong priority level");
            }

            _priorityLevel = (CpuPriorityLevel)priorityLevel;
        }

        public void Execute()
        {
            _oldPriorityLevel = CpuPriorityOptionsWorker.Update(_key, _priorityLevel);
            Console.WriteLine($"Priority level for {_key}" +
                $" update from {_oldPriorityLevel} to {_priorityLevel}");
        }

        public void Undo()
        {
            CpuPriorityOptionsWorker.Update(_key, _oldPriorityLevel);
            Console.WriteLine($"Update for {_key} has been canceled." +
                $" Сurrent priority level: {_oldPriorityLevel}");
        }
    }
}
