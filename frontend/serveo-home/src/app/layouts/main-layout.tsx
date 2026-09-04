import { Outlet } from "@tanstack/react-router";
import { LayoutProvider } from "@/app/providers/layout-provider";
import { DialogProvider } from "@/shared/context/dialog-context";
import { BaseHeader } from "@/shared/components/base-header";
import { Navbar } from "@/shared/components/navbar";
import { Footer } from "@/shared/components/Footer";

type AuthenticatedLayoutProps = {
  children?: React.ReactNode;
};

export function MainLayout({ children }: AuthenticatedLayoutProps) {
  return (
      <LayoutProvider>
        <BaseHeader />
        <DialogProvider>
          <Navbar />
          {children ?? <Outlet />}
          <Footer />
        </DialogProvider>
      </LayoutProvider>
  );
}
