function test_sqrt

for j = 1:20
    r = rand;
    disp(abs(sqrt(r)-custom_sqrt(r,1e-10)))
end


for j = 1:20
    r = 2000*rand;
    disp(abs(sqrt(r)-custom_sqrt(r,1e-10)))
end

end