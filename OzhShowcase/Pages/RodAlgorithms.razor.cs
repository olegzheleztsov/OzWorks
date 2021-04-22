using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oz.Algorithms.Rod;

namespace OzhShowcase.Pages
{
    public partial class RodAlgorithms
    {
        private int _numberToFactor = 1;
        private int _numberToFactorOptimized = 1;
        
        private IEnumerable<int> _factors = new List<int>();
        private IEnumerable<int> _factorsOptimized = new List<int>();
        
        private string _errorMessage = string.Empty;
        private string _errorMessageOptimized = string.Empty;

        private async Task FindFactors()
        {
            try
            {
                _factors = await Task.Factory.StartNew(() => _numberToFactor.FindFactors()).ConfigureAwait(false);
                StateHasChanged();
            }
            catch (Exception exception)
            {
                _errorMessage = exception.Message;
                StateHasChanged();
            }
        }

        private async Task FindFactorsOptimized()
        {
            try
            {
                _factorsOptimized = await Task.Factory.StartNew(() => _numberToFactorOptimized.FindFactorsImproved())
                    .ConfigureAwait(false);
                StateHasChanged();
            }
            catch (Exception exception)
            {
                _errorMessageOptimized = exception.Message;
                StateHasChanged();
            }
        }
    }
}