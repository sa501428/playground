

nums = zeros(1,100);

nums(1:2) = [1 2];

indx = 2;
while (nums(indx) < 4e6)
    nums(indx+1) = sum(nums(indx-1:indx));
    indx = indx + 1;
end

sum(nums(2:3:end))
