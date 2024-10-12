import { Component, OnInit, AfterViewInit } from '@angular/core';
import { BaseChartDirective } from 'ng2-charts';
import { Chart, registerables } from 'chart.js';  // Importar os registráveis

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [BaseChartDirective],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, AfterViewInit {

  constructor() {
    // Registra todos os componentes necessários do Chart.js
    Chart.register(...registerables);
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    // Total Revenue Chart
    const revenueChart = document.getElementById('revenueChart') as HTMLCanvasElement;
    if (revenueChart) {
      new Chart(revenueChart, {
        type: 'bar',
        data: {
          labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
          datasets: [
            {
              label: 'Online Sales',
              data: [12000, 15000, 8000, 18000, 13000, 17000, 20000],
              backgroundColor: '#007bff'
            },
            {
              label: 'Offline Sales',
              data: [10000, 12000, 14000, 13000, 11000, 15000, 16000],
              backgroundColor: '#28a745'
            }
          ]
        }
      });
    }

    // Customer Satisfaction Chart
    const satisfactionChart = document.getElementById('satisfactionChart') as HTMLCanvasElement;
    if (satisfactionChart) {
      new Chart(satisfactionChart, {
        type: 'line',
        data: {
          labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
          datasets: [
            {
              label: 'Last Month',
              data: [3000, 3200, 4000, 3500],
              borderColor: '#6c757d',
              fill: false
            },
            {
              label: 'This Month',
              data: [4000, 4200, 4500, 4700],
              borderColor: '#17a2b8',
              fill: false
            }
          ]
        }
      });
    }

    // Target vs Reality Chart
    const targetRealityChart = document.getElementById('targetRealityChart') as HTMLCanvasElement;
    if (targetRealityChart) {
      new Chart(targetRealityChart, {
        type: 'bar',
        data: {
          labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
          datasets: [
            {
              label: 'Reality Sales',
              data: [9000, 8000, 8500, 9200, 8700, 8900, 8800],
              backgroundColor: '#ffc107'
            },
            {
              label: 'Target Sales',
              data: [10000, 9500, 9700, 11000, 9800, 10000, 10500],
              backgroundColor: '#dc3545'
            }
          ]
        }
      });
    }
  }
}
