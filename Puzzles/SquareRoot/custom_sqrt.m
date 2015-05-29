%
% Custom square root function given number and tolerance
%

function current_val = custom_sqrt(n, tol)

imaginary_i = 1;
if (n < 0)
    imaginary_i = 1i;
    n = -n;
end
    

current_val = n/2;
val_squared = current_val^2;
current_err = abs(val_squared-n);

% [current_val, current_err, current_delta_n]
values = [current_val, current_err, current_val/2];


while (values(2) > tol)
    sign = 1;
    if(values(1)*values(1) > n)
        sign = -1;
    end
    values = ...
        evaluate_new_option(values(1), values(2), sign*values(3),n);
    
end
current_val = imaginary_i*values(1);
return

%
%
%
function output = evaluate_new_option(current_val, current_error, current_delta_n, n)

new_val = current_val + current_delta_n;
new_err = abs(new_val*new_val-n);

if(new_err >= current_error)
    current_delta_n = abs(current_delta_n/2);
else
    current_val = new_val;
    current_error = new_err;
end

output = [current_val, current_error, abs(current_delta_n)];

return

%
%
%
function val = calculate_error(a,b)
val = abs((a-b)*(a+b));
return