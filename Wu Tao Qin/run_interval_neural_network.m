%
% Muhammad S Shamim
%
% Generates figures 1 & 2 for
% 
% Robust exponential stability for interval neural networks with delays and
% non-Lipschitz activation functions
% by Huaiqin Wu · Feng Tao · Leijie Qin · Rui Shi · Lijun He
%
% To run the code, type "run_interval_neural_network" in command prompt
%

function run_interval_neural_network

% diagonal matrix of neural self-inhibitions
% D_low = diag([0.5 0.5]);
% D_high = diag([2 3]);
D = diag([1 1]);

% delayed interconnection weight limits
% B_low = [2 3; 1 2];
% B_high = -B_low;
B = [1 2; 0 1];

% interconnection weight limits
% A_low = -[166 0; 1 167];
% A_high = [-164 2; 1 -165];
A = [-165 1; 0 -166];

% external input
I = [0;0];
tau = 1;

% set variables for figure 1 (initial conditions)
t0 = -tau:0.001:0;
x1 = 0.3*sin(5*t0);
x2 = -0.9*cos(5*t0);
data_record = process_neural_diff_eq(t0, x1, x2, A, B, D, I, tau);

% plot figure 1
figure(1)
plot(data_record(1,:), data_record(2:3,:))
title('figure 1')
legend('x1','x2')

% set variables for figure 2 (initial conditions)
t0 = -tau:0.001:0;
x1 = 0.4*sin(5*t0);
x2 = -0.6*cos(5*t0);
data_record = process_neural_diff_eq(t0, x1, x2, A, B, D, I, tau);

% plot figure 2
figure(2)
plot(data_record(1,:), data_record(2:3,:))
title('figure 2')
legend('x1','x2')
    
end

%
% function for numerically solving differential equation as described in
% the paper
%

function data_record = process_neural_diff_eq(t_record, x1_record,...
    x2_record,  A, B, D, I, tau)

data_record = [t_record;x1_record;x2_record];
iter = length(t_record);
old_iter = 1;    
[~,~]=ode23(@neural_diff_eq, [0 8], data_record(2:3,old_iter)); %integrate

    % differential equations (data record used to maintain results)
    function dxdt = neural_diff_eq(t,x)
        
        old_iter = find(data_record(1,:) > t-tau,1);
        old_val = data_record(2:3,old_iter);
        
        data_record(:,iter+1) = [t;x];
        iter = iter + 1;
        
        dxdt = -D*x + A*g(x) + B*g(old_val) + I;
    end

end

%
% Neuron activation function
%

function val = g(theta)
val = theta.^3;
end

