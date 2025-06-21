document.addEventListener('DOMContentLoaded', function () {
    // input with id "username" on change event
    document.getElementById('username').addEventListener('input', function () {
        const username = this.value;
        // regex to check if username has at least 1 capital letter, 1 special character, and is at least 8 characters long
        const regex = /^(?=.*[A-Z])(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$/;
        if (regex.test(username)) {
            // set the username input border to green
            this.style.borderColor = 'green';
        } else {
            // set the username input border to red
            this.style.borderColor = 'red';
        }
    });

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

    // Send chart image via email when "Send via Email" button is clicked
    document.getElementById('email-btn').addEventListener('click', function () {
        const email = document.getElementById('email-address').value;
        const canvas = document.getElementById('barChart');
        const image = canvas.toDataURL('image/png');

        fetch('/send-chart-email', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, image })
        })
        .then(response => response.json())
        .then(data => {
            alert(data.message || 'Email sent!');
        })
        .catch(() => {
            alert('Failed to send email.');
        });
    });

    // Update barChart when user switches to the Chart tab
    document.getElementById('chart-tab').addEventListener('shown.bs.tab', function () {
        updateBarChart();
    });
});