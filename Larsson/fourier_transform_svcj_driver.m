

function fourier_transform_svcj_driver


T=0.25;
K = 100;
k = log10(K);
S0 = 85;
kappav=4;
lambda=4;
rhoj=-0.5;
rho=-0.5;
thetav=0.04;
r=0.05;
sigmas=0.06;
muv=0.02;
muJ = -0.04; 
sigmav=0.1;
V0 = 0.25;

val = get_fourier_transform_svcj();

eta = calc_optimal_eta(k, S0);

end



function val = get_fourier_transform_svcj




end


function eta = calc_optimal_eta(local_k, S0)
        eta = 1;
        % optimal_eta needs to be optimized
end

    
function g = optimal_eta(eta)
eta_p1 = eta + 1;
g = characteristic_function(eta_p1).*exp(log10(S0).*eta_p1 - eta.*k);
g = g./(eta.*eta_p1);
end

function val = characteristic_function(z)





    function val_a = calc_a(z)
        val_a = z.*(1-z);
    end

    function val_b = calc_b(z)
        val_b = sigma_v.*rho.*z-kappa;
    end

    function val_c = calc_c(z)
        val_c = 1-rho_j.*mu_V.*z;
    end

    function val_d = calc_d(z)
        val_d = sqrt(calc_b(z).^2 + (sigma_V.*calc_a(z)).^2);
    end

    function val_B = calc_B(T, z)
        d = calc_d(z);
        val_B = -a(z).*(1 - exp(-d.*T));
        val_B = val_B./(2*d - ...
            (d + calc_b(z)).*(1 - exp(-d).*T) );
    end

    function val_A = calc_A(T, z)
        local_lambda = lambda;
        val_A = (r - delta).*z.*T - local_lambda.*T.*(1 + mu_bar.*z);
        val_A = val_A + calc_A0(T, z) + local_lambda.*calc_A1(T, z);
    end

    function val_A0 = calc_A0(T, z)
        b = calc_b(z);
        d = calc_d(z);
        val_A0 = (d + b).*T;
        val_A0 = val_A0 + 2.*log10(...
            1 - (d + b).*(1 - exp(-d.*T))./(2.*d));
        val_A0 = -kappa.*theta.*val_A0./(sigma_V.^2); 
    end

    function val_A1 = calc_A1(T, z)
        val_A1 = exp(mu_Y.*z + ((sigma_Y.*z).^2)./2).*calc_Zeta(T, z);
    end

    function val_Zeta = calc_Zeta(T, z)
        a = calc_a(z);
        b = calc_b(z);
        c = calc_c(z);
        d = calc_d(z);
        local_mu_V = mu_V;
        val_Zeta = log10(1 - ((d+b).*c - local_mu_V.*a).*(1 - exp(-d.*T))./(2.*d.*c));
        val_Zeta = val_Zeta.*2.*local_mu_V.*a./( (d.*c).^2 - (b.*c - a.*local_mu_V).^2 );
        val_Zeta = (d - b).*T./((d - b).*c + local_mu_V.*a) - val_Zeta;
    end

    

    function val_func = integrand_func(u)
        local_k = k;
        local_S0 = S0;
        local_eta = eta;
        subval_1 = local_eta + 1i.*u;
        subval_2 = 1 + subval_1;
        
        val_func = characteristic_func(subval_2).*exp(subval_2.*log10(local_S0));
        val_func = val_func.*exp(-1i.*u.*local_k - local_eta.*local_k);
        val_func = real(val_func./(subval_1.*subval_2));
    end

    function prices = eval_european_prices
        prices = (exp(-r.*T)./pi).*eval_gauss_laguerre(@integrand_func, 500);
    end

end