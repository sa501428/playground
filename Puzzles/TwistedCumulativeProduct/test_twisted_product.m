function test_twisted_product

for i=1:20
    a = rand(1,3+ceil(30*rand));
    b = prod(a)./a; 
    disp(sum(twisted_cumulative_product(a) - b))
end

end