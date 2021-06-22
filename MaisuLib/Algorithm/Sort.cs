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
      if (array.Length < 2) {
        return array;
      }

      int mid = array.Length / 2;
      int[] left = new int[mid];
      int[] right = new int[array.Length - mid];
      for (int i = 0; i < left.Length; i++) {
        left[i] = array[i];
      }
      for (int i = 0; i < right.Length; i++) {
        right[i] = array[i + mid];
      }

      left = Merge(left);
      right = Merge(right);
      return Merge(left, right);
    }

    private static int[] Merge(int[] left, int[] right) {
      int pos = 0, left_pos = 0, right_pos = 0;
      int size = left.Length + right.Length;
      int[] result = new int[size];

      while(pos < size) {
        if (left_pos < left.Length && right_pos < right.Length) {
          if (left[left_pos] < right[right_pos]) {
            result[pos++] = left[left_pos++];
          } else {
            result[pos++] = right[right_pos++];
          }
        } else if (left_pos < left.Length) {
          result[pos++] = left[left_pos++];
        } else if (right_pos < right.Length) {
          result[pos++] = right[right_pos++];
        }
      }

      return result;
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
