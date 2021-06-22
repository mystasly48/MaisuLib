using System;

namespace MaisuLib.Algorithm {
  /// <summary>
  /// Sort Class
  /// </summary>
  public class Sort {
    /// <summary>
    /// Bubble Sort Ascending Order
    /// Time Complexity: O(N^2)
    /// </summary>
    /// <param name="array">Sort array (int type)</param>
    /// <returns>Sorted array</returns>
    public static int[] Bubble(int[] array) {
      if (array.Length < 2) {
        return array;
      }

      for (int i = 0; i < array.Length; i++) {
        for (int j = 0; j < array.Length - 1; j++) {
          if (array[j] > array[j + 1]) {
            Swap(ref array[j], ref array[j + 1]);
          }
        }
      }

      return array;
    }

    private static void Swap(ref int a, ref int b) {
      int temp = a;
      a = b;
      b = temp;
    }

    /// <summary>
    /// Heap Sort Ascending Order
    /// Time Complexity: O(NlogN)
    /// </summary>
    /// <param name="array">Int Array</param>
    /// <returns>Sorted Int Array</returns>
    public static int[] Heap(int[] array) {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Merge Sort Ascending Order
    /// Time Complexity: O(NlogN)
    /// </summary>
    /// <param name="array">Int Array</param>
    /// <returns>Sorted Int Array</returns>
    public static int[] Merge(int[] array) {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Quick Sort Ascending Order
    /// Time Complexity: O(NlogN)
    /// </summary>
    /// <param name="array">Int Array</param>
    /// <returns>Sorted Int Array</returns>
    public static int[] Quick(int[] array) {
      throw new NotImplementedException();
    }
  }
}
