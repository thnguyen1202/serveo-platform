import './index.css';
import './i18n';

import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { RouterProvider } from 'react-router-dom';

import { initializeHttp } from '@/core/http';

import { QueryProvider } from './core/query/query-provider.tsx';
//import App from './App.tsx';
import { router } from './routes/index.tsx';

initializeHttp();

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <QueryProvider>
      <RouterProvider router={router} />
    </QueryProvider>
  </StrictMode>,
);
