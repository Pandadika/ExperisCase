
/// <summary>
/// Initilization Begin
/// </summary>
// Database mappen ligges i exe folderen, for at gøre det nemt.
// Load in data
string path = Directory.GetCurrentDirectory() + "/DB/";
string[] Users = File.ReadAllLines(path + "Users.txt");
string[] Products = File.ReadAllLines(path + "Products.txt");
string[] CurrentUserSession = File.ReadAllLines(path + "CurrentUserSession.txt");
Random random = new Random();

// Create user list with user objects
List<User> UsersList = new List<User>();
foreach (var item in Users)
{
    string[] values = item.Split(',');
    List<int> shown  = new List<int>();
    List<int> bought = new List<int>();

    foreach (string value in values[2].Split(';'))
    {
        shown.Add(int.Parse(value));
    }
    foreach (string value in values[3].Split(';'))
    {
        bought.Add(int.Parse(value));
    }

    UsersList.Add(
        new User(int.Parse(values[0]), values[1], shown , bought)
        );
}

//Create film databse as product objects
Library FilmLibrary = new Library();
FilmLibrary.Films = new List<Product>();
foreach (var item in Products)
{
    string[] substring = item.Split(',');
    List<string> genres = new List<string>();
    //Adding keywords
    for (int i = 3; i <= 7; i++)
    {
        if (!string.IsNullOrWhiteSpace(substring[i]))
        {
            genres.Add(substring[i].Capitalize());
        }
    }

    FilmLibrary.Films.Add(
        new Product(int.Parse(substring[0]), substring[1], int.Parse(substring[2]), genres, double.Parse(substring[8],System.Globalization.CultureInfo.InvariantCulture), double.Parse(substring[9]))
        );
}

// adding number of times showed and bought to products
int ids = Products.Length + 1;
foreach (User user in UsersList)
{
    foreach (int boughtItem in user._boughtList)
    {
        for (int id = 1; id < ids; id++)
        {
            if (boughtItem == id)
            {
                FilmLibrary.Films[id - 1].TimesBought++;
            }
        }
    }
    foreach (int shownItem in user._shownList)
    {
        for (int id = 1; id < ids; id++)
        {
            if (shownItem == id)
            {
                FilmLibrary.Films[id - 1].TimesViewed++;
            }
        }
    }
}

// Making list of UserSessions
List<UserSession> userSessions = new List<UserSession>();
foreach (var item in CurrentUserSession)
{
    string[] values = item.Split(',');
    userSessions.Add(new UserSession() {userId = int.Parse(values[0]), viewing = int.Parse(values[1])}); 
}


/// <summary>
/// Inilization end
/// </summary>



Console.WriteLine("");
Console.WriteLine("Finding users hopefully");
foreach (var item in userSessions)
{
    Console.WriteLine(UsersList.Find(u => u._id.Equals(item.userId))._name+" Viewing "+FilmLibrary.Films.Find(p => p.id.Equals(item.viewing)).name);
    Functions.OtherUsersAlso(item.userId, item.viewing, UsersList, FilmLibrary);
}