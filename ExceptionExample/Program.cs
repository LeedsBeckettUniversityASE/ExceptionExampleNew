using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Starting application...\n");

        try
        {
            // Level 1 (Outermost entry point)
            Level1();
        }
        catch (Exception ex)
        {
            Console.WriteLine("=== Caught at Top Level (UI / Program Main) ===");

            // Traverse the inner exception chain to show appended info at each level
            Exception? current = ex;
            int level = 1;

            while (current != null)
            {
                Console.WriteLine($"* {current.GetType().Name}: {current.Message}");
                current = current.InnerException;
                level++;
            }
        }
    }

    // Level 1: Application / Service Layer
    static void Level1()
    {
        try
        {
            Level2();
        }
        catch (Exception ex)
        {
            // Append high-level context and rethrow
            throw new InvalidOperationException("Level 1: Failed to complete user checkout.", ex);
        }
    }

    // Level 2: Business Logic / Repository Layer
    static void Level2()
    {
        try
        {
            Level3();
        }
        catch (Exception ex)
        {
            // Append mid-level context and rethrow
            throw new ApplicationException("Level 2: Database transaction failed during order processing.", ex);
        }
       
       
    }

    // Level 3: Low-Level / Data Access Layer (Source of exception)
    static void Level3()
    {
        // Simulate a low-level network or database timeout
        throw new TimeoutException("Level 3: Connection to SQL Server instance timed out after 30 seconds.");
    }
}
