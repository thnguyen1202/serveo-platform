import { env } from "@/shared/config/env";
import axios from "axios";
import { ApiException } from "./api.exception";

export const apiClient = axios.create({
  baseURL: env.apiUrl,
  withCredentials: true, // QUAN TRỌNG: Cho phép gửi và nhận HTTP-Only Cookie
  timeout: 10000, // 10 giây cho tất cả các API thông thường
  headers: {
    "Content-Type": "application/json",
  },
});



const aipkeyProtectedEndpoints = ["/auth/refresh", "/auth/login", "/auth/csrf"];

apiClient.interceptors.request.use(async (config) => {
  const url = config.url ?? "";

  const requiresApikey = aipkeyProtectedEndpoints.some((endpoint) => url.startsWith(endpoint));
  if (requiresApikey) {
    config.headers["X-API-KEY"] = env.apiKey;
  }


  return config;
});

apiClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    console.dir("apiClient:interceptors.response:error", error);

    // Kiểm tra nếu lỗi là do Timeout
    if (error.code === "ECONNABORTED" && error.message.includes("timeout")) {
      // alert(t('error.request.timeout'));
      alert(
        "Network connection is too slow or the server is not responding. Please try again later!",
      );
    }

    if (axios.isCancel(error)) {
      return Promise.reject(error);
    }

    return Promise.reject(handleHttpError(error));
  },
);

function handleHttpError(error: unknown) {
  if (import.meta.env.VITE_ENABLE_LOG === "true") {
    console.dir("handleHttpError", error);
  }

  if (axios.isAxiosError(error)) {
    const problem = error.response?.data;
    if (problem) {
      return new ApiException(problem, error.status);
    }
  }

  return error;
}
