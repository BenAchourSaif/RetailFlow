import { apiGet } from "./client";

export interface HealthStatus {
  status: string;
  timestamp: string;
}

export function getHealth() {
  return apiGet<HealthStatus>("/api/health");
}