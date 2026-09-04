import "react-i18next";
// Import file ngôn ngữ mặc định (thường là file chuẩn của dự án)
import vi from "@/i18n/locales/vi/common.json";

declare module "react-i18next" {
  interface CustomTypeOptions {
    // Đặt namespace mặc định
    defaultNS: "common";
    // Khai báo tài nguyên ngôn ngữ mặc định để TS tự suy luận kiểu
    resources: {
      common: typeof vi;
    };
  }
}
