public static class HostingService {

    public static async Task HostDinnerAsync() {
        // Going to clean before doing anything else
        await PrepCleanAsync();

        SetTable();

        // If you cannot do anything else until chicken is done cooking, await here.
        // Chicken cookedChicken = await CookChickenAsync();

        // If you would like to do other things while chicken cooks, capture the Task returned.
        Task<Chicken> cookingChickenTask = CookChickenAsync();

        LiftWeights();
        // SomethingElse();

        // Must have the chicken done being cooked before cutting it
        Chicken cookedChicken = await cookingChickenTask;
        CutUpChicken(cookedChicken, 4);
    }

    public static void SetTable() {
        Console.WriteLine("Table is set.");
    }

    public static void LiftWeights() {
        Console.WriteLine("Lifted weights!");
    }

    /***********************************************
                HOUSE CLEANING TASKS BEGIN
    ***********************************************/

    public static async Task PrepCleanAsync() {
        Console.WriteLine("Cleaning the house...");
        Task dishesTask = CleanDishesAsync();

        Vaccuum();
        Mop();

        await dishesTask;

        Console.WriteLine("House is clean.");
    }

    public static void Vaccuum() {
        Console.WriteLine("Vaccuumed.");
    }

    public static void Mop() {
        Console.WriteLine("Mopped.");
    }

    /***********************************************
                HOUSE CLEANING TASKS END
    ***********************************************/

    /***********************************************
                    DISHES TASKS BEGIN
    ***********************************************/
    public static async Task CleanDishesAsync() {
        LoadDishwasher();
        await StartDishwasherAsync();
        PutDishesAway();
    }

    public static void LoadDishwasher() {
        Console.WriteLine("Loaded dishwasher.");
    }

    public static async Task StartDishwasherAsync() {
        Console.WriteLine("*-*-*-*-* Washing dishes...\n");
        await Task.Delay(5000);
        Console.WriteLine("\n*-*-*-*-* Dishes clean.");
    }

    public static void PutDishesAway() {
        Console.WriteLine("Dishes put away.");
    }

    /***********************************************
                    DISHES TASKS END
    ***********************************************/

    public static async Task<Chicken> CookChickenAsync() {
        Console.WriteLine("*-*-*-*-* Chicken is in the oven...\n");
        await Task.Delay(5000);
        Console.WriteLine("\n*-*-*-*-*Chicken is cooked!");

        return new Chicken { SizeInPounds = 40, IsDone = true };
    }

    public static int CutUpChicken(Chicken chicken, int numPlates) {
        Console.WriteLine("Cutting up the chicken...");
        Console.WriteLine("Chicken is cut and ready to serve!");

        return chicken.SizeInPounds / numPlates;
    }
}