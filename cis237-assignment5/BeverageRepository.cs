using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace cis237_assignment5
{
    //*********************************************************************************
    // Repository API Class 
    //*********************************************************************************
    class BeverageRepository
    {
        // Create an instance of the BeverageContext class
        BeverageContext _beverageContext = new BeverageContext();

        // Create an instance of the Beverage class
        Beverage _beverage = new Beverage();

        // Find a Beverage by Id
        public string FindById(string Id)
        {
            // Pull the Beverage from the collection based on primary key
            Beverage _beverageById = _beverageContext.Beverages.Find(Id);

            // Declare return string for the found item
            string returnString = null;

            // If the beverage is not null
            if (_beverageById != null)
            {
                // If the beverage Id is the same as the search Id
                if (_beverageById.id == Id)
                {
                    // Set the return string to the result
                    // of the beverage's ToString method.
                    returnString = BeverageToString(_beverageById);
                }
            }

            // Return the returnString
            return returnString;
        }


        // Add a new Beverage to the Database
        public void AddNewItem(
            string Id,
            string Name,
            string Pack,
            decimal Price,
            bool Active
        )
        {
            // Make a new Beverage to send properties to
            Beverage newBeverageToAdd = new Beverage();

            // Pass in new Beverage properties
            newBeverageToAdd.id = Id;
            newBeverageToAdd.name = Name;
            newBeverageToAdd.pack = Pack;
            newBeverageToAdd.price = Price;
            newBeverageToAdd.active = Active;

            // Try catch to ensure user can't add a Beverage with an id that already exists
            try
            {
                // Add the new beverage to the Beverages Collection
                _beverageContext.Beverages.Add(newBeverageToAdd);

                // Print new item
                Console.WriteLine(BeverageToString(newBeverageToAdd));
                Console.WriteLine();

                // Save the changes to the database
                _beverageContext.SaveChanges();
            }
            // Could not update Database exception
            catch (DbUpdateException e)
            {
                // Remove the new Beverage from the Beverages Collection since we cant save it.
                _beverageContext.Beverages.Remove(newBeverageToAdd);
                // Write to console that there was an error.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cant add the record. Already have one with that Primary Key");
            }
            // Any other exception caught here
            catch (Exception e)
            {
                // Remove the new Beverage from the Beverages Collection since we cant save it.
                _beverageContext.Beverages.Remove(newBeverageToAdd);
                // Write to console that there was an error.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cant add the record. Unknown error occured");
            }
        }

        // Update a Beverage from the collection
        public void UpdateBeverage(
            string Id,
            string Name,
            string Pack,
            decimal Price,
            bool Active)
        {
            try
            {
                // Pull the Beverage from the collection to send new properties to
                Beverage beverageToUpdate = _beverageContext.Beverages.Find(Id);

                // Display Beverage being updated 
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Updating:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(BeverageToString(beverageToUpdate));
                Console.WriteLine();

                // Updated Beverage information passed in
                // Except for Id
                beverageToUpdate.name = Name;
                beverageToUpdate.pack = Pack;
                beverageToUpdate.price = Price;
                beverageToUpdate.active = Active;

                // Save changes to the Database
                _beverageContext.SaveChanges();

                // Let the user know the Database has been updated
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Update Successful");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(BeverageToString(beverageToUpdate));
                Console.WriteLine();
            }
            catch (Exception e)
            {
                // Write to console that there was an error.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cant add the record. Unknown error occured");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        // Delete an existing Beverage from the Database
        public void DeleteBeverage(string Id)
        {
            // Get the Beverage out of the database
            Beverage beverageToDelete = _beverageContext.Beverages.Find(Id);

            // Print out Beverage getting deleted
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Deletion in progress...");
            Console.ForegroundColor = ConsoleColor.Gray;
            BeverageToString(beverageToDelete);
            Console.WriteLine();

            // Remove Beverage from Beverage collection
            _beverageContext.Beverages.Remove(beverageToDelete);

            // Save changes to the Database
            _beverageContext.SaveChanges();

            // Verify the Beverage is deleted
            beverageToDelete = _beverageContext.Beverages.Find(Id);
            if (beverageToDelete == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Deletion complete");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                // Condition should not be met
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Beverage Not Deleted");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    
        // Returns formatted Beverage string
        public string BeverageToString(Beverage beverage)
        {
            return $"{beverage.id}  {beverage.name}{beverage.pack}${beverage.price:F2} {beverage.active}";
        }

        // Method to convert the collection to a string
        public void BevString()
        {
            // Loop through all of the Beverages
            foreach (Beverage beverage in _beverageContext.Beverages)
            {
                Console.WriteLine(BeverageToString(beverage));
            }
        }
    }
    //*********************************************************************************
}
