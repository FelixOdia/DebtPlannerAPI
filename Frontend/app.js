document.getElementById("debtForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const username = document.getElementById("username").value;
    const income = parseFloat(document.getElementById("income").value);
    const debtElements = document.querySelectorAll(".debt");

    const debts = [];
    debtElements.forEach((debt, i) => {
        debts.push({
            id: i + 1,
            name: debt.querySelector(".debtName").value,
            balance: parseFloat(debt.querySelector(".debtBalance").value),
            interestRate: parseFloat(debt.querySelector(".debtRate").value),
            minimumPayment: parseFloat(debt.querySelector(".debtMin").value),
            userId: 1,
            user: {
                id: 1,
                username: username,
                passwordHash: "YWJjMTIz", // dummy base64
                passwordSalt: "eHl6NDU2", // dummy base64
                monthlyIncome: [income]
            }
        });
    });

    const user = {
        id: 1,
        username: username,
        passwordHash: "YWJjMTIz", // dummy
        passwordSalt: "eHl6NDU2", // dummy
        monthlyIncome: [income]
    };

    const body = JSON.stringify({ user, debts });

    try {
        const res = await fetch("https://localhost:7200/api/plan/generate", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: body
        });

        if (!res.ok) throw new Error("API error");

        const data = await res.json();

        const resultDiv = document.getElementById("result");
        resultDiv.innerHTML = "";

        data.forEach((plan) => {
            resultDiv.innerHTML += `
        <div>
          <strong>${plan.debtName}</strong><br />
          Payment Date: ${new Date(plan.paymentDate).toLocaleDateString()}<br />
          Payment Amount: ₦${plan.paymentAmount.toFixed(2)}<br />
          Interest Paid: ₦${plan.interestPaid.toFixed(2)}<br />
          Principal Paid: ₦${plan.principalPaid.toFixed(2)}<br />
          Remaining Balance: ₦${plan.remainingBalance.toFixed(2)}<br />
          <hr />
        </div>
      `;
        });
    } catch (err) {
        alert("Something went wrong. Make sure your API is running.");
        console.error(err);
    }
});

function addDebt() {
    const container = document.getElementById("debtsContainer");
    const debtHTML = `
    <div class="debt">
      <label>Name: <input type="text" class="debtName" required /></label>
      <label>Balance: <input type="number" class="debtBalance" required /></label>
      <label>Interest Rate: <input type="number" class="debtRate" required /></label>
      <label>Minimum Payment: <input type="number" class="debtMin" required /></label>
      <hr />
    </div>
  `;
    container.insertAdjacentHTML("beforeend", debtHTML);
}
