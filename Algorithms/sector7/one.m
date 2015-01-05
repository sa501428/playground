
all_nums = 1:999;

acceptable = zeros(1,999);
acceptable(3:3:999) = 1;
acceptable(5:5:999) = 1;

sum(acceptable.*all_nums)

