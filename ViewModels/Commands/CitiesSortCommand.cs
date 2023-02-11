using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Stores.Address;

namespace ViewModels.Commands;

public class CitiesSortCommand
{
    private List<City> _cities;
    public CitiesSortCommand(List<City> cities)
    {
        _cities = cities;
    }
    private int partition(int low, int high)
    {
        City temp;
        City pivot = _cities[high];

        // index of smaller element
        int i = (low - 1);
        for (int j = low; j <= high - 1; j++)
        {

            // If current element is
            // smaller than or
            // equal to pivot
            if (String.Compare(_cities[j].ToString(), pivot.ToString()) <= 0)
            {
                i++;
                // swap _cities[i] and _cities[j]
                temp = _cities[i];
                _cities[i] = _cities[j];
                _cities[j] = temp;
            }
        }

        // swap _cities[i+1] and _cities[high]
        // (or pivot)
        temp = _cities[i + 1];
        _cities[i + 1] = _cities[high];
        _cities[high] = temp;

        return i + 1;
    }

    /* The main function that implements
    QuickSort() _cities[] --> Array to be 
    sorted,
    low --> Starting index,
    high --> Ending index */
    private void qSort(int low, int high)
    {
        if (low < high)
        {
            /* pi is partitioning index, 
            _cities[pi] is now at right place */
            int pi = partition(low, high);

            // Recursively sort elements
            // before partition and after
            // partition
            qSort(low, pi - 1);
            qSort(pi + 1, high);
        }
    }
    public List<City> GetSortedCities()
    {
        qSort(0, _cities.Count - 1);
        return _cities;
    }
}
