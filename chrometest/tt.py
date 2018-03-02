class Solution:
    def checkPossibility(self, nums):
        """
        :type nums: List[int]
        :rtype: bool
        """
        n = len(nums)
        count = 0
        for i in range(n-1):
            if nums[i+1] < nums[i]:
                if i > 0:
                    if nums[i-1] > nums[i+1]:
                        count += 1
                else:
                    count += 1
                if i+1 < n-1:
                    if nums[i] < nums[i+2]:
                        count += 1
                else:
                    count += 1
            if count > 1:
                return False
        return True
if __name__ == '__main__':
    s = Solution()
    a = [4,2,3]
    b = [1,2,5,2,3]
    c = [4,5,1,6,6]
    print(s.checkPossibility(a))
    print(s.checkPossibility(b))
    