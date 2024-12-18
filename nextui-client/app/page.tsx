import { Link } from "@nextui-org/link";
import { button as buttonStyles } from "@nextui-org/theme";
import RegisterModal from "@/components/RegisterModal";
import LoginForm from "@/components/LoginForm";

export default function Home() {
  return (
    <section className="flex flex-col items-center justify-center gap-8 py-8 md:py-10">
      <div className="flex flex-col gap-8 max-w-4xl text-center justify-center">
        <span className="text-5xl lg:text-7xl font-semibold tracking-normal leading-loose">Create your personal newsfeed.</span>
        <div className="text-2xl leading-9">
          Follow the news, events, and people you care about. No interference. No algorithm.
        </div>
      </div>

      <div className="flex gap-3">
        <RegisterModal fontWeight="semibold" buttonSize="md"/>
        <Link
          isExternal={false}
          className={buttonStyles({ variant: "bordered", color: "primary" }) + " font-semibold btn"}
          href="/features"
        >
          See the Features
        </Link>
      </div>
    </section>
  );
}