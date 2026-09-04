import { create } from "zustand";
import { persist } from "zustand/middleware";

interface MenuState {
  menuId: string | null;
  setMenuId: (id: string) => void;
  clearMenuId: () => void;
}

export const useMenuStore = create<MenuState>()(
  persist(
    (set) => ({
      menuId: null, // Giá trị mặc định khi chưa chọn menu
      setMenuId: (id: string) => set({ menuId: id }),
      clearMenuId: () => set({ menuId: null }),
    }),
    {
      name: "active-menu-storage", // Tên key lưu trong localStorage
    },
  ),
);
