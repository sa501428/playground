
function five
n = 1;

for i = 20:-1:2
    if(mod(n,i) ~= 0)
        n = safe_adjust(n, i);
    end
end

disp(n)

return

function n = safe_adjust(n, i)
factors_n = factor(n);
factors_i = factor(i);

for indx = 1:length(factors_i)
    found = find(factors_n == factors_i(indx),1);
    if (~isempty(found))
        factors_n(found) = 0;
    else
        n = n*factors_i(indx);
    end
end

return
