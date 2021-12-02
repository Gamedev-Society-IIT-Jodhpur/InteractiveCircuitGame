using System.Collections;
using System.Collections.Generic; 

public class AvailableRoutes
{
    //public static string root = "https://circuit-simulator-iitj.herokuapp.com/";
    public static string root = "http://localhost:4040/";
    public static string  userResults = root + "api/result/userResults";
    public static string allResults = root + "api/result/allResults";

    public static string googleSignup = root;

    public static string checkUser = root + "api/profile/checkUser?email=";

    public static string updateUser = root + "api/profile/updateUser";

    public static string availableItems = root + "api/item/availableItems";

}
