import { MutationCache, QueryCache, QueryClient } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { toast } from "sonner";
import { ApiException } from "../axios/api.exception";

export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 1000 * 60 * 5, // Dữ liệu được xem là mới: 5 phút
      gcTime: 1000 * 60 * 30, // Cache giữ trong memory: 30 phút
      refetchOnWindowFocus: import.meta.env.PROD,
      retry: (failureCount, error) => {
        if (import.meta.env.DEV) console.log({ failureCount, error });

        if (failureCount >= 0 && import.meta.env.DEV) return false;
        if (failureCount > 3 && import.meta.env.PROD) return false;

        return !(error instanceof AxiosError && [401, 403].includes(error.response?.status ?? 0));
      },
    },

    mutations: {
      onError: (error) => {
        console.error("mutations:error", error);
        if (error instanceof AxiosError) {
          if (error.response?.status === 304) {
            toast.error("Content not modified!");
          }
        }
      },
    },
  },
  queryCache: new QueryCache({
    onError: (error) => {
      console.error("queryCache", error);
    },
  }),
  mutationCache: new MutationCache({
    onError(error) {
      if (import.meta.env.VITE_ENABLE_LOG === "true") {
        console.dir("mutationCache", error);
      }

      if (error instanceof ApiException) {
        if (error.problem.instance?.includes("/auth/login")) {
          let description = "";
          if (Array.isArray(error.problem.errors)) {
            description = error.problem.errors.map((x) => x.message).join(" \n");
          }
          toast.error(error.message, {
            description: description,
            descriptionClassName: "whitespace-pre-line leading-relaxed", // Helps preserve '\n' formatting and adds line spacing
          });
        }
      } else {
        toast.error("Unexpected error", {
          description: error.message,
        });
      }
    },
  }),
});
