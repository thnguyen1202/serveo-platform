import { useAuth } from '@/features/auth/hooks/use-auth';

export default function Header() {
  const { user, logout } = useAuth();

  return (
    <header
      className="
h-16
border-b
flex
items-center
justify-between
px-6
"
    >
      <div>Serveo</div>

      <div
        className="
flex
items-center
gap-4
"
      >
        <span>{user?.displayName}</span>

        <button onClick={logout}>Logout</button>
      </div>
    </header>
  );
}

// export default function Header(){

//     return (
//         <header
//             className="
//             h-16
//             border-b
//             flex
//             items-center
//             justify-between
//             px-6
//             "
//         >

//             <h1 className="font-semibold">
//                 Admin Portal
//             </h1>

//             <div>
//                 Admin User
//             </div>

//         </header>
//     );
// }
