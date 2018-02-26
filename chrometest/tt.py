class Solution:
    def maxSubArray(self, nums):
        """
        :type nums: List[int]
        :rtype: int
        """
        import numpy as np
        import operator as o
        snums = np.cumsum(nums)
        maxSum = snums.max()
        minSum = snums.min()
        maxIndex = o.indexOf(snums, maxSum)
        minIndex = o.indexOf(snums, minSum)
        if maxIndex == 0:
            result = snums[0]
        else:
            if maxIndex < minIndex:
                snums = snums[:maxIndex]
                minSum = snums.min()
                minIndex = o.indexOf(snums, minSum)
            if maxIndex > minIndex:
                if minSum > 0:
                    result = maxSum
                else:
                    result = maxSum - minSum
            elif maxIndex == minIndex:
                result = maxSum
        return  int(result)

if __name__ == "__main__":
    s = Solution()
    a = [1,-1,-2]
    print(s.maxSubArray(a))

