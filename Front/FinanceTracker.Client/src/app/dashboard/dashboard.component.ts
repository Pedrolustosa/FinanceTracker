import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, AfterViewInit {

  constructor() {
    Chart.register(...registerables);
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    // Monthly Income & Expenses Chart
    const incomeExpenseChart = document.getElementById('incomeExpenseChart') as HTMLCanvasElement;
    if (incomeExpenseChart) {
      new Chart(incomeExpenseChart, {
        type: 'bar',
        data: {
          labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
          datasets: [
            {
              label: 'Income',
              data: [1200, 1400, 1300, 1500, 1600, 1700, 1800],
              backgroundColor: '#4caf50'
            },
            {
              label: 'Expenses',
              data: [900, 1000, 950, 1200, 1100, 1050, 950],
              backgroundColor: '#f44336'
            }
          ]
        }
      });
    }

    // Savings Progress Chart
    const savingsProgressChart = document.getElementById('savingsProgressChart') as HTMLCanvasElement;
    if (savingsProgressChart) {
      new Chart(savingsProgressChart, {
        type: 'line',
        data: {
          labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
          datasets: [
            {
              label: 'Savings',
              data: [300, 350, 400, 450, 500, 550, 600],
              borderColor: '#3f51b5',
              fill: false
            }
          ]
        }
      });
    }
  }
}
