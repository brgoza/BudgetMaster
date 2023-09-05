namespace BudgetMaster.Common
{

    public static class Statistics
    {
        public static double CalculateStandardDeviation(List<double> sample)
        {
            if (sample == null || sample.Count < 2)
            {
                throw new ArgumentException("Sample must have at least two elements");
            }

            // Calculate the mean of the sample
            double mean = sample.Average();

            // Calculate the squared differences from the mean
            var squaredDifferences = sample.Select(x => Math.Pow(x - mean, 2)).ToList();

            // Calculate the average of the squared differences
            double avgSquaredDifference = squaredDifferences.Average();

            // Calculate the standard deviation
            double standardDeviation = Math.Sqrt(avgSquaredDifference);

            return standardDeviation;
        }
    }

}
