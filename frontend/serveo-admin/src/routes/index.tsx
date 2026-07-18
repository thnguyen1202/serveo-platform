import { createBrowserRouter } from 'react-router-dom';

import LoginPage from '@/features/auth/pages/login-page';
import AdminLayout from '@/layouts/AdminLayout';

import ProtectedRoute from './protected-route';

export const router = createBrowserRouter([
  {
    path: '/login',
    element: <LoginPage />,
  },
  {
    path: '/',
    element: (
      <ProtectedRoute>
        <AdminLayout />
      </ProtectedRoute>
    ),
    children: [
      {
        index: true,
        element: <div>Dashboard</div>,
      },
      {
        path: 'users',
        element: <div>Users</div>,
      },
    ],
  },
]);
