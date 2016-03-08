LeetCode Problem Link: https://leetcode.com/problems/two-sum/

Solution Source Link: https://github.com/Twiknight/LeetCode-in-Fsharp/blob/master/code/two-sum.fs

Problem:
>Given an array of integers, return indices of the two numbers such that they add up to a specific target.

>You may assume that each input would have exactly one solution.

So first, Let's do some translation:
 1. We have an array of several integers, assume it is `arr`
 2. We have an integer, assume it is `target`
 3. Our target is, find two integers `num1` and `num2` in  the array and `num1+num2 == target`
 4. We are sure we can find `num1` and `num2`

It's easy, right?
We can just iterate the array and try to confirm that:
for each integer `i` whether we have an integer equals `target-i`

here comes the code:
```fsharp
let twoSum (nums:int[]) target =
    let length = nums.Length
    [for x in 0..length-2 do
        for y in x+1..length-1 do
            if nums.[x]+nums.[y] = target then
                yield (x,y)].Head
```

The time complexity is O(n^2)

Can we make it faster?

I'm not sure, but we know at least it would be O(n) cost as we need to iterate through the array.

So if there is any possible way to optimize the algorithm, it must be something about the inner loop.
In the inner loop, we iterate through the array and try to find an integer which equals `target-currentInt`.

In other words, we were __looking up__ and specific integer.

That's it! For lookup, we do have some O(1) structs, namely __HashMap__ or __Dictionary__.

If we replace the inner loop with a `Map`, we'll be able to optimize it to O(n).

Here goes the code:
```fsharp
let twoSum (nums:int[]) target =
    let rec search (map:Map<int,int>) (arr:int[]) idx =
        if map.ContainsKey(target-arr.[idx]) then (map.Item(target-arr.[idx]),idx)
        else search (map.Add(arr.[idx],idx)) arr (idx+1)
    search Map.empty nums 0
```
