function B = twisted_cumulative_product(A)


B = zeros(size(A));

B(1) = A(2);
B(2) = A(1);
running_product = A(1)*A(2);

for i=3:length(A)
    B = B*A(i);
    B(i) = running_product;
    running_product = running_product*A(i);
end


end