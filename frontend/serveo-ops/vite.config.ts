import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import path from "path";
import TanStackRouter from "@tanstack/router-plugin/vite";
import tailwindcss from "@tailwindcss/vite";

// https://vite.dev/config/
export default defineConfig({
  plugins: [TanStackRouter({routesDirectory: './src/app/routes',}), react(), tailwindcss()],
  resolve: {
    alias: {
      "@": path.resolve(import.meta.dirname, "./src"),
    },
  },
});
