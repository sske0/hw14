using Core.Constants;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;
using System.Globalization;

namespace Presentation
{
    public static class Program
    {
        static void Main()
        {
            GroupRepository _groupRepository = new GroupRepository();
            ConsoleHelper.WriteWithColor("--- Welcome! ---", ConsoleColor.Cyan);

            while (true)
            {
                ConsoleHelper.WriteWithColor("1 - Group Creation", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("2 - Update Group", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("3 - Delete Group", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("4 - Get All Groups", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("5 - Get Group By Id", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("6 - Get Group By Name", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("0 - Exit", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("--- Select option ---", ConsoleColor.Cyan);
                int number;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed number's format is not valid", ConsoleColor.Red);
                }
                else
                {
                    if (!(number >= 0 && number <= 6))
                    {
                        ConsoleHelper.WriteWithColor("Choose a number from 0 to 6!", ConsoleColor.Red);
                    }
                    else
                    {
                        switch(number)
                        {
                            case (int)GroupOptions.GroupCreation:
                                ConsoleHelper.WriteWithColor("-- Enter name: ", ConsoleColor.DarkCyan);
                                string name = Console.ReadLine();

                                MaxSizeInput:  ConsoleHelper.WriteWithColor("-- Enter max size of the group: ", ConsoleColor.DarkCyan);
                                int maxSize;
                                isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("Inputed size format is not valid", ConsoleColor.Red);
                                    goto MaxSizeInput;
                                }

                                if(maxSize > 20)
                                {
                                    ConsoleHelper.WriteWithColor("Max size of group is 20", ConsoleColor.Red);
                                    goto MaxSizeInput;
                                }
                                
                                StartDateInput: ConsoleHelper.WriteWithColor("-- Enter start date: ", ConsoleColor.DarkCyan);
                                DateTime startDate;
                                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("Start date's format is not correct!", ConsoleColor.Red);
                                    goto StartDateInput;
                                }

                                DateTime boundaryDate = new DateTime(2015, 1, 1);

                                if (startDate < boundaryDate)
                                {
                                    ConsoleHelper.WriteWithColor("Start date is not chosen right", ConsoleColor.Red);
                                    goto StartDateInput;
                                }

                                EndDateInput: ConsoleHelper.WriteWithColor("-- Enter end date: ", ConsoleColor.DarkCyan);
                                DateTime endDate;
                                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("Start date's format is not correct!", ConsoleColor.Red);
                                    goto EndDateInput;
                                }

                                if(startDate > endDate)
                                {
                                    ConsoleHelper.WriteWithColor("End date cant be earlier than start date!", ConsoleColor.Red);
                                    goto EndDateInput;
                                }

                                var group = new Group
                                {
                                    Name = name,
                                    MaxSize= maxSize,
                                    StartDate = startDate,
                                    EndDate = endDate,
                                };

                                _groupRepository.Add(group);
                                ConsoleHelper.WriteWithColor($"Group was successfully created with Name: {group.Name}\n Max size: {group.MaxSize}\n Start date: {group.StartDate.ToLongDateString()}\n End date: {group.EndDate.ToLongDateString()}", ConsoleColor.Magenta);


                                break;
                            case (int)GroupOptions.UpdateGroup:
                                    break;
                            case (int)GroupOptions.DeleteGroup:
                                var groupss = _groupRepository.GetAll();
                                foreach (var group_ in groupss)
                                {
                                    ConsoleHelper.WriteWithColor($"Id: {group_.Id} Name: {group_.Name} Max size: {group_.MaxSize}, Start date: {group_.StartDate} End date: {group_.EndDate}", ConsoleColor.Magenta);
                                }
                                IdInput: ConsoleHelper.WriteWithColor("-- Enter Id: ", ConsoleColor.DarkCyan);

                                int id;
                                isSucceeded = int.TryParse(Console.ReadLine(), out id);
                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("Id's format is not correct!", ConsoleColor.Red);
                                    goto IdInput;
                                }

                                var dbGroup = _groupRepository.Get(id);
                                if (dbGroup is null)
                                    ConsoleHelper.WriteWithColor("There is no such a group with written id", ConsoleColor.Red);
                                else
                                {
                                    _groupRepository.Delete(dbGroup);
                                    ConsoleHelper.WriteWithColor("The group was successfully deleted", ConsoleColor.Green);
                                }


                                break;
                            case (int)GroupOptions.GetAllGroups:
                                var groups = _groupRepository.GetAll();
                                foreach (var group_ in groups) 
                                {
                                    ConsoleHelper.WriteWithColor($"Id: {group_.Id} Name: {group_.Name} Max size: {group_.MaxSize}, Start date: {group_.StartDate} End date: {group_.EndDate}", ConsoleColor.Magenta);
                                }
                                break;
                            case (int)GroupOptions.GetGroupById:
                                break;
                            case (int)GroupOptions.GetGroupByName:
                                break;
                            case (int)GroupOptions.Exit:
                                return;

                            default:
                                break;
                        }

                    }
                }
            }     

        }
    }
}