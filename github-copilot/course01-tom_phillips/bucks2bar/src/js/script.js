document.addEventListener('DOMContentLoaded', function () {
    const ctx = document.getElementById('barChart').getContext('2d');
    const barChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [
                'January', 'February', 'March', 'April', 'May', 'June',
                'July', 'August', 'September', 'October', 'November', 'December'
            ],
            datasets: [{
                label: 'Income',
                data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                backgroundColor: 'rgba(54, 162, 235, 0.7)'
            }, {
                label: 'Expense',
                data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                backgroundColor: 'rgba(255, 99, 132, 0.7)'
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: { beginAtZero: true }
            }
        }
    });

    function getMonthlyData() {
        const months = [
            'january', 'february', 'march', 'april', 'may', 'june',
            'july', 'august', 'september', 'october', 'november', 'december'
        ];
        const income = [];
        const expense = [];
        months.forEach(month => {
            const incomeInput = document.getElementById(`income-${month}`);
            const expenseInput = document.getElementById(`expense-${month}`);
            income.push(incomeInput ? Number(incomeInput.value) || 0 : 0);
            expense.push(expenseInput ? Number(expenseInput.value) || 0 : 0);
        });
        return { income, expense };
    }

    // Update barChart data with current input values
    function updateBarChart() {
        const data = getMonthlyData();
        barChart.data.datasets[0].data = data.income;
        barChart.data.datasets[1].data = data.expense;
        barChart.update();
    }

    // Add event listeners to all income and expense inputs
    document.querySelectorAll('input[id^="income-"], input[id^="expense-"]').forEach(input => {
        input.addEventListener('input', updateBarChart);
    });

    // Download chart as image when "Download" button is clicked
    document.getElementById('download-btn').addEventListener('click', function () {
        const canvas = document.getElementById('barChart');
        const link = document.createElement('a');
        link.href = canvas.toDataURL('image/png');
        link.download = 'bucks2bar-chart.png';
        link.click();
    });
});