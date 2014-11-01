
function val = eval_gauss_laguerre(func_handle, n)

[laguerre_polynomial_n, laguerre_polynomial_n_plus_1] = ...
    calc_laguerre_polynomial(n);

x=roots(laguerre_polynomial_n);
func_eval = feval(func_handle, x).*exp(x);

% Laguerre weights
weights = x./ (n+1)^2;
weights = weights ./ polyval(laguerre_polynomial_n_plus_1,x).^2;

val = dot(func_eval, weights);

end


function [coeff_n, coeff_n_plus_1] = calc_laguerre_polynomial(n)

k = 0:n+1;
k = (-1.^k)./factorial(k);

coeff_n = fliplr(k(1:n+1).*binomial_coefficients(n));
coeff_n_plus_1 = fliplr(k.*binomial_coefficients(n+1));

end

function coeff = binomial_coefficients(n)

coeff = 0:n;
coeff = ((n-coeff) ./ (coeff+1));
coeff = cumprod(coeff);
coeff = [1 coeff(1:end-1)];

end