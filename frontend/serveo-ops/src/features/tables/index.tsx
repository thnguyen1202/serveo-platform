import { Content } from '@/shared/components/layout/main';
import { Dialogs } from './components/dialogs';
import { TablesPage } from './pages/tables-page';

export function Tables() {
  return (
    <Content>
      <TablesPage />
      <Dialogs />
    </Content>
  );
}
