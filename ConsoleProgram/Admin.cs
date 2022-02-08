static class Admin
{
    public static bool isAdmin()
    {
        const string adminPass = "123421";

        System.Console.WriteLine("Please enter Admin password: ");
        string response = System.Console.ReadLine();
        if (response != adminPass)
        {
            System.Console.WriteLine("Incorrect password, press any key to continue");
            System.Console.ReadKey();
            return false;
        }
        else return true;
    }
}
