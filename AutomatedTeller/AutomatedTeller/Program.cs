using System;

namespace AutomatedTeller
{
    public class Program
    {
        static void Main(string[] args)
        {
            decimal currentBalance = 547.31M;
            bool sessionActive = true;

            Console.WriteLine("Welcome to the automated teller machine!");

            // Run a finite state machine until the user chooses to exit
            while (sessionActive)
            {
                DisplayMenu();

                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        currentBalance = WithdrawPrompt(currentBalance);
                        break;
                    case '2':
                        currentBalance = DepositPrompt(currentBalance);
                        break;
                    case '3':
                        Console.WriteLine($"\nYour current balance is {ViewBalance(currentBalance)}");
                        break;
                    case '4':
                        sessionActive = false;
                        break;
                    default:
                        continue;
                } 
            }
        }

        /// <summary>
        /// Displays the ATM menu to the user using the console
        /// </summary>
        static void DisplayMenu()
        {
            Console.WriteLine("\nPlease choose an option below:");
            Console.WriteLine("1) Withdraw Funds");
            Console.WriteLine("2) Deposit Funds");
            Console.WriteLine("3) View Account Balance");
            Console.WriteLine("4) End Session");
        }

        /// <summary>
        /// Prompts the user for an amount to withdraw and then attempts to withdraw
        /// that amount. Returns the original balance upon any errors.
        /// </summary>
        /// <param name="currentBalance">The user's current balance</param>
        /// <returns>The user's account balance after making the withdrawal</returns>
        static decimal WithdrawPrompt(decimal currentBalance)
        {
            Console.WriteLine($"\nYour current balance is {ViewBalance(currentBalance)}");
            Console.WriteLine("How much would you like to withdraw?");
            Console.WriteLine("Please only type numbers and decimal points.");

            try
            {
                return WithdrawFunds(currentBalance, ReadUserDecimal());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return currentBalance;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return currentBalance;
            }
        }

        /// <summary>
        /// Prompts the user for an amount to deposit and returns the new
        /// account balance after performing the deposit
        /// </summary>
        /// <param name="currentBalance">The user's current account balance</param>
        /// <returns>The account balance after performing the deposit</returns>
        static decimal DepositPrompt(decimal currentBalance)
        {
            Console.WriteLine("\nHow much would you like to deposit?");
            Console.WriteLine("Please only type numbers and decimal points.");

            try
            {
                return DepositFunds(currentBalance, ReadUserDecimal());
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return currentBalance;
            }
        }

        /// <summary>
        /// Convenience method which attempts to read a decimal number from the console.
        /// Only allows values between 0.0 and System.Decimal.MaxValue
        /// </summary>
        /// <returns>Decimal struct containing the user's input</returns>
        /// <exception cref="OverflowException">The user entered too large o</exception>
        static decimal ReadUserDecimal()
        {
            decimal amount;

            // Keep trying to convert input to a decimal until a correctly formatted
            // string has been input between 0.0 and System.Decimal.MaxValue
            for (;;)
            {
                try
                {
                    amount = Convert.ToDecimal(Console.ReadLine());
                }
                catch (OverflowException)
                {
                    Console.WriteLine("\nUser entered too large or small of a number.");
                    Console.WriteLine("Please try again with a number between " +
                        $"0 and {decimal.MaxValue}");
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nCould not understand the user's input.");
                    Console.WriteLine("Please try again, ensuring that only numbers" +
                        " and decimal points are included.");
                    continue;
                }

                // This method is meant to only return values of at least 0.0
                if (amount >= 0.0M)
                {
                    break;
                }

                Console.WriteLine("\nPlease try again with a number greater than 0.0");
            }

            return amount;
        }

        /// <summary>
        /// Returns the balance that would remain after withdrawing
        /// <paramref name="withdrawalAmount"/> from <paramref name="currentBalance"/>.
        /// Does not allow overdrafting.
        /// </summary>
        /// <param name="currentBalance">The user's current account balance</param>
        /// <param name="withdrawalAmount">The amount the user would like
        /// to withdraw from his/her account</param>
        /// <returns>The new account balance after withdrawing funds</returns>
        /// <exception cref="ArgumentOutOfRangeException">Passing a negative number
        /// to <paramref name="withdrawalAmount"/> is not allowed. Use DepositFunds()
        /// instead.</exception>
        /// <exception cref="InvalidOperationException">This method will not allow
        /// the user to overdraft his/her account by passing a <paramref name="withdrawalAmount"/>
        /// value greater than <paramref name="currentBalance"/></exception>
        public static decimal WithdrawFunds(decimal currentBalance,
            decimal withdrawalAmount)
        {
            if (withdrawalAmount < 0.0M)
            {
                throw new ArgumentOutOfRangeException("withdrawalAmount",
                    "Expected a positive number for the withdrawal amount. Use " +
                    "DepositFunds() to add money to an account.");
            }
            if (withdrawalAmount > currentBalance)
            {
                throw new InvalidOperationException("Unable to withdraw more " +
                    "funds than the user's account currently holds.");
            }

            return currentBalance - withdrawalAmount;
        }

        /// <summary>
        /// Returns the balance that would exist after depositing
        /// <paramref name="depositAmount"/> to <paramref name="currentBalance"/>
        /// </summary>
        /// <param name="currentBalance">The user's current account balance</param>
        /// <param name="depositAmount">The amount that the user would like to add
        /// to his/her account</param>
        /// <returns>The new account balance after depositing funds</returns>
        /// <exception cref="ArgumentOutOfRangeException">Passing a negative number
        /// to <paramref name="depositAmount"/> is not allowed. Use WithdrawFunds()
        /// to remove money from an account.</exception>
        public static decimal DepositFunds(decimal currentBalance,
            decimal depositAmount)
        {
            if (depositAmount < 0.0M)
            {
                throw new ArgumentOutOfRangeException(nameof(depositAmount),
                    "Expected a positive number for the deposit amount. Use " +
                    "WithdrawFunds() to remove money from an account.");
            }

            return currentBalance + depositAmount;
        }

        /// <summary>
        /// Converts the user's current balance to a string in the current
        /// locale's currency format
        /// </summary>
        /// <param name="currentBalance">The user's current balance</param>
        /// <returns>A string representation of the user's current balance in the
        /// current locale's currency format</returns>
        public static string ViewBalance(decimal currentBalance)
        {
            return currentBalance.ToString("C");
        }
    }
}
