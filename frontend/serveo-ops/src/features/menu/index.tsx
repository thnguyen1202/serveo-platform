import { Content } from '@/shared/components/layout/main';
import { MenuPage } from './pages/menu-page';
import { Dialogs } from './components/dialogs';

export function Menu() {
  return (
    <Content>
      <MenuPage />
      <Dialogs />
    </Content>
  );
}
