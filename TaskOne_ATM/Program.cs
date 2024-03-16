
public class ATM
{
    //Array of denominations
    private static readonly int[] Denominations = { 10,50,100 };

    //This function call the FindCombinations function
    public static List<List<int>> CalculateCombinations(int targetAmount)
    {
        //This is the List of allCombinations for a each targetAmount
        List<List<int>> allCombinations = new List<List<int>>();
        FindCombinations(targetAmount, new List<int>(), allCombinations);
        return allCombinations;
    }

    // Function to calculate combinations for a given target amount
    private static void FindCombinations(int remainingAmount, List<int> currentCombination, List<List<int>> allCombinations)
    {
        
        // Base case: If the target amount becomes 0, a combination is found
        if (remainingAmount == 0)
        {
            //If combination already there inside the allCombinations rhen did not add that and skip
            var orderdList = currentCombination.OrderDescending().ToList();
            if (!allCombinations.Any(list=>list.SequenceEqual(orderdList)))
            {
                allCombinations.Add(new List<int>(currentCombination.OrderDescending()));
                return;
            }
        }
        // Base case: If the target amount becomes negative or there are no more denominations left
        if (remainingAmount < 0)
        {
            return;
        }
        foreach (int denomination in Denominations)
        {
            if (denomination <= remainingAmount)
            {
                currentCombination.Add(denomination);
                //Function Recursive Call
                FindCombinations(remainingAmount - denomination, currentCombination, allCombinations);
                // Remove last added denomination to explore other combinations
                currentCombination.RemoveAt(currentCombination.Count - 1);
            }
        }
    }

    public static void Main(string[] args)
    {
        int[] amounts = {30,50,60,80,140,230,370,610,980};

        foreach (int amount in amounts)
        {
            List<List<int>> combinations = CalculateCombinations(amount);
            Console.WriteLine($"For {amount} EUR, the available payout denominations would be:");
            foreach (List<int> combination in combinations)
            {
                //count number of Denominations 
                Dictionary<int, int> denominationCounts = combination.GroupBy(e => e)
                                               .ToDictionary(g => g.Key, g => g.Count());
                string output = string.Join(" + ", denominationCounts.Select(kv => $"{kv.Value} x {kv.Key} euro"));

                Console.WriteLine($"• {output}");
            }
            Console.WriteLine();
        }
    }
}
