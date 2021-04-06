using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237_assignment5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set Console Window Size
            Console.BufferHeight = Int16.MaxValue - 1;
            Console.WindowHeight = 40;
            Console.WindowWidth = 160;

            // Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            // Create an instance of the BeverageRepository class
            BeverageRepository _beverageRepository = new BeverageRepository();

            // Display the Welcome Message to the user
            userInterface.DisplayWelcomeGreeting();

            // Display the Menu and get the response. Store the response in the choice integer
            // This is the 'primer' run of displaying and getting.
            int choice = userInterface.DisplayMenuAndGetResponse();

            //*************************************
            // While the choice is not exit program
            //*************************************
            while (choice != 6)
            {
                switch (choice)
                {
                    case 1:
                        // Print Entire List Of Items
                        userInterface.DisplayAllItems();
                        _beverageRepository.BevString();
                        break;

                    case 2:
                        // Search For An Item
                        string searchQuery = userInterface.GetSearchQuery();
                        string itemInformation = _beverageRepository.FindById(searchQuery);
                        if (itemInformation != null)
                        {
                            userInterface.DisplayItemFound(itemInformation);
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;

                    case 3:
                        // Add A New Item To The List
                        string[] newItemInformation = userInterface.GetNewItemInformation();
                        if (_beverageRepository.FindById(newItemInformation[0]) == null)
                        {
                            _beverageRepository.AddNewItem(
                                newItemInformation[0],
                                newItemInformation[1],
                                newItemInformation[2],
                                decimal.Parse(newItemInformation[3]),
                                (newItemInformation[4] == "True")
                            );
                            userInterface.DisplayAddWineItemSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemAlreadyExistsError();
                        }
                        break;

                    case 4:
                        // Update Item From The List
                        string[] updateItemInformation = userInterface.GetUpdatedItemInformation();
                        if (_beverageRepository.FindById(updateItemInformation[0]) != null)
                        {
                            _beverageRepository.UpdateBeverage(
                                updateItemInformation[0],
                                updateItemInformation[1],
                                updateItemInformation[2],
                                decimal.Parse(updateItemInformation[3]),
                                (updateItemInformation[4] == "True")
                            );
                            userInterface.DisplayAddWineItemSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;

                    case 5:
                        // Remove Item From The List
                        string deleteSearchQuery = userInterface.GetSearchQuery();
                        string deletionItem = _beverageRepository.FindById(deleteSearchQuery);
                        if (deletionItem != null)
                        {
                            // Display item to be deleted
                            userInterface.DisplayItemFound(deletionItem);

                            // Make sure the user wants to delete
                            if (userInterface.DeleteVerification(deletionItem) == true)
                            {
                                _beverageRepository.DeleteBeverage(deleteSearchQuery);
                            }
                            else
                            {
                                // Display when "n" is selected, user did not want to delete
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Deletion Aborted");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;
                }

                // Get the new choice of what to do from the user
                choice = userInterface.DisplayMenuAndGetResponse();
            }
        }
    }
}
