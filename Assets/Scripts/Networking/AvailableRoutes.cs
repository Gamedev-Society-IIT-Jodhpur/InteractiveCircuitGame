public class AvailableRoutes
{
    public static string root = "http://circuit-maverick.herokuapp.com/";
    // public static string root = "http://localhost:4040/";

    public static string userResults = root + "api/result/userResults";
    public static string allResults = root + "api/result/allResults";
    public static string addResult = root + "api/result/addResult";


    public static string googleSignup = root;


    public static string getXP = root + "api/profile/getXP";
    public static string setXP = root + "api/profile/updateXP";



    public static string checkUser = root + "api/profile/checkUser?email=";
    public static string updateUser = root + "api/profile/updateUser";


    public static string availableItems = root + "api/item/availableItems";
    public static string availableGames = root + "api/game/getAvailableGames";
}
