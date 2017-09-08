/* Generated By:JJTree&JavaCC: Do not edit this line. TsurgeonParser.java */
package old.edu.stanford.nlp.trees.tregex.tsurgeon;

import java.io.StringReader;
import old.edu.stanford.nlp.trees.*;
import old.edu.stanford.nlp.util.Pair;
import java.util.*;


class TsurgeonParser/*@bgen(jjtree)*/implements TsurgeonParserTreeConstants, TsurgeonParserConstants {/*@bgen(jjtree)*/
  protected static JJTTsurgeonParserState jjtree = new JJTTsurgeonParserState();
  private static TreeFactory treeFactory = new LabeledScoredTreeFactory();

  static TsurgeonPattern parse(String s) throws ParseException {
    if (jj_initialized_once) {
      ReInit(new StringReader(s + "\n"));
    } else {
      new TsurgeonParser(new StringReader(s + "\n"));
    }
    return Root();
  }


  public static void main(String[] args) {
    System.out.println("Reading from standard input...");
    TsurgeonParser t = new TsurgeonParser(System.in);
    try {
      TsurgeonPattern n = t.Root();
      System.out.println(n.toString());
      System.out.println("Thank you.");
    } catch (Exception e) {
      System.out.println("Oops.");
      System.out.println(e.getMessage());
      e.printStackTrace();
    }
  }

  static final public TsurgeonPattern Root() throws ParseException {
 /*@bgen(jjtree) Root */
  SimpleNode jjtn000 = new SimpleNode(JJTROOT);
  boolean jjtc000 = true;
  jjtree.openNodeScope(jjtn000);TsurgeonPattern result;
    try {
      result = Operation();
      jj_consume_token(24);
    jjtree.closeNodeScope(jjtn000, true);
    jjtc000 = false;
    {if (true) return result;}
    } catch (Throwable jjte000) {
    if (jjtc000) {
      jjtree.clearNodeScope(jjtn000);
      jjtc000 = false;
    } else {
      jjtree.popNode();
    }
    if (jjte000 instanceof RuntimeException) {
      {if (true) throw (RuntimeException)jjte000;}
    }
    if (jjte000 instanceof ParseException) {
      {if (true) throw (ParseException)jjte000;}
    }
    {if (true) throw (Error)jjte000;}
    } finally {
    if (jjtc000) {
      jjtree.closeNodeScope(jjtn000, true);
    }
    }
    throw new Error("Missing return statement in function");
  }

  static final public TsurgeonPattern Operation() throws ParseException {
 /*@bgen(jjtree) Operation */
  SimpleNode jjtn000 = new SimpleNode(JJTOPERATION);
  boolean jjtc000 = true;
  jjtree.openNodeScope(jjtn000);TsurgeonPattern child1;
  TsurgeonPattern child2 = null;
  Token newLabel = null;
  TreeLocation loc = null;
  Token operator;
  AuxiliaryTree tree = null;
  List nodeSelections = null;
  Token regex;
  Token hash_int;
    try {
      switch ((jj_ntk==-1)?jj_ntk():jj_ntk) {
      case DELETE:
        operator = jj_consume_token(DELETE);
        nodeSelections = NodeSelectionList(new ArrayList());
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new DeleteNode(nodeSelections);}
        break;
      case PRUNE:
        operator = jj_consume_token(PRUNE);
        nodeSelections = NodeSelectionList(new ArrayList());
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new PruneNode(nodeSelections);}
        break;
      case EXCISE:
        operator = jj_consume_token(EXCISE);
        child1 = NodeSelection();
        child2 = NodeSelection();
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new ExciseNode(child1,child2);}
        break;
      default:
        jj_la1[0] = jj_gen;
        if (jj_2_1(3)) {
          operator = jj_consume_token(RELABEL);
          child1 = NodeSelection();
          newLabel = jj_consume_token(IDENTIFIER);
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new RelabelNode(child1,newLabel.image);}
        } else if (jj_2_2(3)) {
          operator = jj_consume_token(RELABEL);
          child1 = NodeSelection();
          newLabel = jj_consume_token(QUOTEX);
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new RelabelNode(child1, newLabel.image);}
        } else {
          switch ((jj_ntk==-1)?jj_ntk():jj_ntk) {
          case RELABEL:
            operator = jj_consume_token(RELABEL);
            child1 = NodeSelection();
            regex = jj_consume_token(REGEX);
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new RelabelNode(child1, regex.image);}
            break;
          default:
            jj_la1[1] = jj_gen;
            if (jj_2_3(2)) {
              operator = jj_consume_token(REPLACE);
              child1 = NodeSelection();
              tree = TreeRoot(false);
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new ReplaceNode(child1,tree);}
            } else {
              switch ((jj_ntk==-1)?jj_ntk():jj_ntk) {
              case REPLACE:
                operator = jj_consume_token(REPLACE);
                child1 = NodeSelection();
                child2 = NodeSelection();
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new ReplaceNode(child1,child2);}
                break;
              case MOVE:
                operator = jj_consume_token(MOVE);
                child1 = NodeSelection();
                loc = Location();
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new MoveNode(child1, loc);}
                break;
              default:
                jj_la1[2] = jj_gen;
                if (jj_2_4(2)) {
                  operator = jj_consume_token(INSERT);
                  child1 = NodeSelection();
                  loc = Location();
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new InsertNode(child1, loc);}
                } else {
                  switch ((jj_ntk==-1)?jj_ntk():jj_ntk) {
                  case INSERT:
                    operator = jj_consume_token(INSERT);
                    tree = TreeRoot(false);
                    loc = Location();
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new InsertNode(tree, loc);}
                    break;
                  case ADJOIN:
                    operator = jj_consume_token(ADJOIN);
                    tree = TreeRoot(true);
                    child1 = NodeSelection();
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new AdjoinNode(tree, child1);}
                    break;
                  case ADJOIN_TO_HEAD:
                    operator = jj_consume_token(ADJOIN_TO_HEAD);
                    tree = TreeRoot(true);
                    child1 = NodeSelection();
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new AdjoinToHeadNode(tree, child1);}
                    break;
                  case ADJOIN_TO_FOOT:
                    operator = jj_consume_token(ADJOIN_TO_FOOT);
                    tree = TreeRoot(true);
                    child1 = NodeSelection();
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new AdjoinToFootNode(tree, child1);}
                    break;
                  case COINDEX:
                    operator = jj_consume_token(COINDEX);
                    nodeSelections = NodeSelectionList(new ArrayList());
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new CoindexNodes((TsurgeonPattern[]) nodeSelections.toArray(new TsurgeonPattern[] {}));}
                    break;
                  default:
                    jj_la1[3] = jj_gen;
                    jj_consume_token(-1);
                    throw new ParseException();
                  }
                }
              }
            }
          }
        }
      }
    } catch (Throwable jjte000) {
    if (jjtc000) {
      jjtree.clearNodeScope(jjtn000);
      jjtc000 = false;
    } else {
      jjtree.popNode();
    }
    if (jjte000 instanceof RuntimeException) {
      {if (true) throw (RuntimeException)jjte000;}
    }
    if (jjte000 instanceof ParseException) {
      {if (true) throw (ParseException)jjte000;}
    }
    {if (true) throw (Error)jjte000;}
    } finally {
    if (jjtc000) {
      jjtree.closeNodeScope(jjtn000, true);
    }
    }
    throw new Error("Missing return statement in function");
  }

  static final public TreeLocation Location() throws ParseException {
 /*@bgen(jjtree) Location */
  SimpleNode jjtn000 = new SimpleNode(JJTLOCATION);
  boolean jjtc000 = true;
  jjtree.openNodeScope(jjtn000);Token rel;
  TsurgeonPattern child;
    try {
      rel = jj_consume_token(LOCATION_RELATION);
      child = NodeSelection();
     jjtree.closeNodeScope(jjtn000, true);
     jjtc000 = false;
     {if (true) return new TreeLocation(rel.image, child);}
    } catch (Throwable jjte000) {
   if (jjtc000) {
     jjtree.clearNodeScope(jjtn000);
     jjtc000 = false;
   } else {
     jjtree.popNode();
   }
   if (jjte000 instanceof RuntimeException) {
     {if (true) throw (RuntimeException)jjte000;}
   }
   if (jjte000 instanceof ParseException) {
     {if (true) throw (ParseException)jjte000;}
   }
   {if (true) throw (Error)jjte000;}
    } finally {
   if (jjtc000) {
     jjtree.closeNodeScope(jjtn000, true);
   }
    }
    throw new Error("Missing return statement in function");
  }

  static final public List NodeSelectionList(List l) throws ParseException {
 /*@bgen(jjtree) NodeSelectionList */
  SimpleNode jjtn000 = new SimpleNode(JJTNODESELECTIONLIST);
  boolean jjtc000 = true;
  jjtree.openNodeScope(jjtn000);TsurgeonPattern result;
    try {
      result = NodeSelection();
                                  l.add(result);
      label_1:
      while (true) {
        switch ((jj_ntk==-1)?jj_ntk():jj_ntk) {
        case IDENTIFIER:
          ;
          break;
        default:
          jj_la1[4] = jj_gen;
          break label_1;
        }
        result = NodeSelection();
                                 l.add(result);
      }
     jjtree.closeNodeScope(jjtn000, true);
     jjtc000 = false;
     {if (true) return l;}
    } catch (Throwable jjte000) {
   if (jjtc000) {
     jjtree.clearNodeScope(jjtn000);
     jjtc000 = false;
   } else {
     jjtree.popNode();
   }
   if (jjte000 instanceof RuntimeException) {
     {if (true) throw (RuntimeException)jjte000;}
   }
   if (jjte000 instanceof ParseException) {
     {if (true) throw (ParseException)jjte000;}
   }
   {if (true) throw (Error)jjte000;}
    } finally {
   if (jjtc000) {
     jjtree.closeNodeScope(jjtn000, true);
   }
    }
    throw new Error("Missing return statement in function");
  }

// we'll also put in a way to use a SELECTION with a list of nodes.
  static final public TsurgeonPattern NodeSelection() throws ParseException {
 /*@bgen(jjtree) NodeSelection */
  SimpleNode jjtn000 = new SimpleNode(JJTNODESELECTION);
  boolean jjtc000 = true;
  jjtree.openNodeScope(jjtn000);TsurgeonPattern result;
    try {
      result = NodeName();
    jjtree.closeNodeScope(jjtn000, true);
    jjtc000 = false;
    {if (true) return result;}
    } catch (Throwable jjte000) {
    if (jjtc000) {
      jjtree.clearNodeScope(jjtn000);
      jjtc000 = false;
    } else {
      jjtree.popNode();
    }
    if (jjte000 instanceof RuntimeException) {
      {if (true) throw (RuntimeException)jjte000;}
    }
    if (jjte000 instanceof ParseException) {
      {if (true) throw (ParseException)jjte000;}
    }
    {if (true) throw (Error)jjte000;}
    } finally {
    if (jjtc000) {
      jjtree.closeNodeScope(jjtn000, true);
    }
    }
    throw new Error("Missing return statement in function");
  }

  static final public TsurgeonPattern NodeName() throws ParseException {
 /*@bgen(jjtree) NodeName */
  SimpleNode jjtn000 = new SimpleNode(JJTNODENAME);
  boolean jjtc000 = true;
  jjtree.openNodeScope(jjtn000);Token t;
    try {
      t = jj_consume_token(IDENTIFIER);
    jjtree.closeNodeScope(jjtn000, true);
    jjtc000 = false;
    {if (true) return new FetchNode(t.image);}
    } finally {
    if (jjtc000) {
      jjtree.closeNodeScope(jjtn000, true);
    }
    }
    throw new Error("Missing return statement in function");
  }

// the argument says whether there must be a foot node on the aux tree.
  static final public AuxiliaryTree TreeRoot(boolean requiresFoot) throws ParseException {
 /*@bgen(jjtree) TreeRoot */
  SimpleNode jjtn000 = new SimpleNode(JJTTREEROOT);
  boolean jjtc000 = true;
  jjtree.openNodeScope(jjtn000);Tree t;
    try {
      t = TreeNode();
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return new AuxiliaryTree(t,requiresFoot);}
    } catch (Throwable jjte000) {
    if (jjtc000) {
      jjtree.clearNodeScope(jjtn000);
      jjtc000 = false;
    } else {
      jjtree.popNode();
    }
    if (jjte000 instanceof RuntimeException) {
      {if (true) throw (RuntimeException)jjte000;}
    }
    if (jjte000 instanceof ParseException) {
      {if (true) throw (ParseException)jjte000;}
    }
    {if (true) throw (Error)jjte000;}
    } finally {
    if (jjtc000) {
      jjtree.closeNodeScope(jjtn000, true);
    }
    }
    throw new Error("Missing return statement in function");
  }

  static final public Tree TreeNode() throws ParseException {
 /*@bgen(jjtree) TreeNode */
 SimpleNode jjtn000 = new SimpleNode(JJTTREENODE);
 boolean jjtc000 = true;
 jjtree.openNodeScope(jjtn000);Token label;
 List dtrs = null;
    try {
      switch ((jj_ntk==-1)?jj_ntk():jj_ntk) {
      case TREE_NODE_NONTERMINAL_LABEL:
        label = jj_consume_token(TREE_NODE_NONTERMINAL_LABEL);
        dtrs = TreeDtrs(new ArrayList());
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return treeFactory.newTreeNode(label.image.substring(1),dtrs);}
        break;
      case IDENTIFIER:
      case TREE_NODE_TERMINAL_LABEL:
        switch ((jj_ntk==-1)?jj_ntk():jj_ntk) {
        case TREE_NODE_TERMINAL_LABEL:
          label = jj_consume_token(TREE_NODE_TERMINAL_LABEL);
          break;
        case IDENTIFIER:
          label = jj_consume_token(IDENTIFIER);
          break;
        default:
          jj_la1[5] = jj_gen;
          jj_consume_token(-1);
          throw new ParseException();
        }
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
      {if (true) return treeFactory.newTreeNode(label.image,new ArrayList());}
        break;
      default:
        jj_la1[6] = jj_gen;
        jj_consume_token(-1);
        throw new ParseException();
      }
    } catch (Throwable jjte000) {
   if (jjtc000) {
     jjtree.clearNodeScope(jjtn000);
     jjtc000 = false;
   } else {
     jjtree.popNode();
   }
   if (jjte000 instanceof RuntimeException) {
     {if (true) throw (RuntimeException)jjte000;}
   }
   if (jjte000 instanceof ParseException) {
     {if (true) throw (ParseException)jjte000;}
   }
   {if (true) throw (Error)jjte000;}
    } finally {
   if (jjtc000) {
     jjtree.closeNodeScope(jjtn000, true);
   }
    }
    throw new Error("Missing return statement in function");
  }

  static final public List TreeDtrs(List dtrs) throws ParseException {
 /*@bgen(jjtree) TreeDtrs */
  SimpleNode jjtn000 = new SimpleNode(JJTTREEDTRS);
  boolean jjtc000 = true;
  jjtree.openNodeScope(jjtn000);Tree tree;
    try {
      switch ((jj_ntk==-1)?jj_ntk():jj_ntk) {
      case IDENTIFIER:
      case TREE_NODE_TERMINAL_LABEL:
      case TREE_NODE_NONTERMINAL_LABEL:
        tree = TreeNode();
        TreeDtrs(dtrs);
      jjtree.closeNodeScope(jjtn000, true);
      jjtc000 = false;
     dtrs.add(0,tree); {if (true) return dtrs;}
        break;
      case 25:
        jj_consume_token(25);
    jjtree.closeNodeScope(jjtn000, true);
    jjtc000 = false;
   {if (true) return dtrs;}
        break;
      default:
        jj_la1[7] = jj_gen;
        jj_consume_token(-1);
        throw new ParseException();
      }
    } catch (Throwable jjte000) {
    if (jjtc000) {
      jjtree.clearNodeScope(jjtn000);
      jjtc000 = false;
    } else {
      jjtree.popNode();
    }
    if (jjte000 instanceof RuntimeException) {
      {if (true) throw (RuntimeException)jjte000;}
    }
    if (jjte000 instanceof ParseException) {
      {if (true) throw (ParseException)jjte000;}
    }
    {if (true) throw (Error)jjte000;}
    } finally {
    if (jjtc000) {
      jjtree.closeNodeScope(jjtn000, true);
    }
    }
    throw new Error("Missing return statement in function");
  }

  static private boolean jj_2_1(int xla) {
    jj_la = xla; jj_lastpos = jj_scanpos = token;
    try { return !jj_3_1(); }
    catch(LookaheadSuccess ls) { return true; }
    finally { jj_save(0, xla); }
  }

  static private boolean jj_2_2(int xla) {
    jj_la = xla; jj_lastpos = jj_scanpos = token;
    try { return !jj_3_2(); }
    catch(LookaheadSuccess ls) { return true; }
    finally { jj_save(1, xla); }
  }

  static private boolean jj_2_3(int xla) {
    jj_la = xla; jj_lastpos = jj_scanpos = token;
    try { return !jj_3_3(); }
    catch(LookaheadSuccess ls) { return true; }
    finally { jj_save(2, xla); }
  }

  static private boolean jj_2_4(int xla) {
    jj_la = xla; jj_lastpos = jj_scanpos = token;
    try { return !jj_3_4(); }
    catch(LookaheadSuccess ls) { return true; }
    finally { jj_save(3, xla); }
  }

  static private boolean jj_3R_2() {
    if (jj_3R_3()) return true;
    return false;
  }

  static private boolean jj_3R_3() {
    if (jj_scan_token(IDENTIFIER)) return true;
    return false;
  }

  static private boolean jj_3_1() {
    if (jj_scan_token(RELABEL)) return true;
    if (jj_3R_2()) return true;
    if (jj_scan_token(IDENTIFIER)) return true;
    return false;
  }

  static private boolean jj_3_3() {
    if (jj_scan_token(REPLACE)) return true;
    if (jj_3R_2()) return true;
    return false;
  }

  static private boolean jj_3_4() {
    if (jj_scan_token(INSERT)) return true;
    if (jj_3R_2()) return true;
    return false;
  }

  static private boolean jj_3_2() {
    if (jj_scan_token(RELABEL)) return true;
    if (jj_3R_2()) return true;
    if (jj_scan_token(QUOTEX)) return true;
    return false;
  }

  static private boolean jj_initialized_once = false;
  /** Generated Token Manager. */
  static public TsurgeonParserTokenManager token_source;
  static SimpleCharStream jj_input_stream;
  /** Current token. */
  static public Token token;
  /** Next token. */
  static public Token jj_nt;
  static private int jj_ntk;
  static private Token jj_scanpos, jj_lastpos;
  static private int jj_la;
  static private int jj_gen;
  static final private int[] jj_la1 = new int[8];
  static private int[] jj_la1_0;
  static {
      jj_la1_init_0();
   }
   private static void jj_la1_init_0() {
      jj_la1_0 = new int[] {0xb0,0x40,0x600,0x7900,0x10000,0x410000,0xc10000,0x2c10000,};
   }
  static final private JJCalls[] jj_2_rtns = new JJCalls[4];
  static private boolean jj_rescan = false;
  static private int jj_gc = 0;

  /** Constructor with InputStream. */
  public TsurgeonParser(java.io.InputStream stream) {
     this(stream, null);
  }
  /** Constructor with InputStream and supplied encoding */
  public TsurgeonParser(java.io.InputStream stream, String encoding) {
    if (jj_initialized_once) {
      System.out.println("ERROR: Second call to constructor of static parser.  ");
      System.out.println("       You must either use ReInit() or set the JavaCC option STATIC to false");
      System.out.println("       during parser generation.");
      throw new Error();
    }
    jj_initialized_once = true;
    try { jj_input_stream = new SimpleCharStream(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e); }
    token_source = new TsurgeonParserTokenManager(jj_input_stream);
    token = new Token();
    jj_ntk = -1;
    jj_gen = 0;
    for (int i = 0; i < 8; i++) jj_la1[i] = -1;
    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
  }

  /** Reinitialise. */
  static public void ReInit(java.io.InputStream stream) {
     ReInit(stream, null);
  }
  /** Reinitialise. */
  static public void ReInit(java.io.InputStream stream, String encoding) {
    try { jj_input_stream.ReInit(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e); }
    token_source.ReInit(jj_input_stream);
    token = new Token();
    jj_ntk = -1;
    jjtree.reset();
    jj_gen = 0;
    for (int i = 0; i < 8; i++) jj_la1[i] = -1;
    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
  }

  /** Constructor. */
  public TsurgeonParser(java.io.Reader stream) {
    if (jj_initialized_once) {
      System.out.println("ERROR: Second call to constructor of static parser. ");
      System.out.println("       You must either use ReInit() or set the JavaCC option STATIC to false");
      System.out.println("       during parser generation.");
      throw new Error();
    }
    jj_initialized_once = true;
    jj_input_stream = new SimpleCharStream(stream, 1, 1);
    token_source = new TsurgeonParserTokenManager(jj_input_stream);
    token = new Token();
    jj_ntk = -1;
    jj_gen = 0;
    for (int i = 0; i < 8; i++) jj_la1[i] = -1;
    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
  }

  /** Reinitialise. */
  static public void ReInit(java.io.Reader stream) {
    jj_input_stream.ReInit(stream, 1, 1);
    token_source.ReInit(jj_input_stream);
    token = new Token();
    jj_ntk = -1;
    jjtree.reset();
    jj_gen = 0;
    for (int i = 0; i < 8; i++) jj_la1[i] = -1;
    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
  }

  /** Constructor with generated Token Manager. */
  public TsurgeonParser(TsurgeonParserTokenManager tm) {
    if (jj_initialized_once) {
      System.out.println("ERROR: Second call to constructor of static parser. ");
      System.out.println("       You must either use ReInit() or set the JavaCC option STATIC to false");
      System.out.println("       during parser generation.");
      throw new Error();
    }
    jj_initialized_once = true;
    token_source = tm;
    token = new Token();
    jj_ntk = -1;
    jj_gen = 0;
    for (int i = 0; i < 8; i++) jj_la1[i] = -1;
    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
  }

  /** Reinitialise. */
  public void ReInit(TsurgeonParserTokenManager tm) {
    token_source = tm;
    token = new Token();
    jj_ntk = -1;
    jjtree.reset();
    jj_gen = 0;
    for (int i = 0; i < 8; i++) jj_la1[i] = -1;
    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
  }

  static private Token jj_consume_token(int kind) throws ParseException {
    Token oldToken;
    if ((oldToken = token).next != null) token = token.next;
    else token = token.next = token_source.getNextToken();
    jj_ntk = -1;
    if (token.kind == kind) {
      jj_gen++;
      if (++jj_gc > 100) {
        jj_gc = 0;
        for (int i = 0; i < jj_2_rtns.length; i++) {
          JJCalls c = jj_2_rtns[i];
          while (c != null) {
            if (c.gen < jj_gen) c.first = null;
            c = c.next;
          }
        }
      }
      return token;
    }
    token = oldToken;
    jj_kind = kind;
    throw generateParseException();
  }

  static private final class LookaheadSuccess extends Error { }
  static final private LookaheadSuccess jj_ls = new LookaheadSuccess();
  static private boolean jj_scan_token(int kind) {
    if (jj_scanpos == jj_lastpos) {
      jj_la--;
      if (jj_scanpos.next == null) {
        jj_lastpos = jj_scanpos = jj_scanpos.next = token_source.getNextToken();
      } else {
        jj_lastpos = jj_scanpos = jj_scanpos.next;
      }
    } else {
      jj_scanpos = jj_scanpos.next;
    }
    if (jj_rescan) {
      int i = 0; Token tok = token;
      while (tok != null && tok != jj_scanpos) { i++; tok = tok.next; }
      if (tok != null) jj_add_error_token(kind, i);
    }
    if (jj_scanpos.kind != kind) return true;
    if (jj_la == 0 && jj_scanpos == jj_lastpos) throw jj_ls;
    return false;
  }


/** Get the next Token. */
  static final public Token getNextToken() {
    if (token.next != null) token = token.next;
    else token = token.next = token_source.getNextToken();
    jj_ntk = -1;
    jj_gen++;
    return token;
  }

/** Get the specific Token. */
  static final public Token getToken(int index) {
    Token t = token;
    for (int i = 0; i < index; i++) {
      if (t.next != null) t = t.next;
      else t = t.next = token_source.getNextToken();
    }
    return t;
  }

  static private int jj_ntk() {
    if ((jj_nt=token.next) == null)
      return (jj_ntk = (token.next=token_source.getNextToken()).kind);
    else
      return (jj_ntk = jj_nt.kind);
  }

  static private List jj_expentries = new ArrayList();
  static private int[] jj_expentry;
  static private int jj_kind = -1;
  static private int[] jj_lasttokens = new int[100];
  static private int jj_endpos;

  static private void jj_add_error_token(int kind, int pos) {
    if (pos >= 100) return;
    if (pos == jj_endpos + 1) {
      jj_lasttokens[jj_endpos++] = kind;
    } else if (jj_endpos != 0) {
      jj_expentry = new int[jj_endpos];
      for (int i = 0; i < jj_endpos; i++) {
        jj_expentry[i] = jj_lasttokens[i];
      }
      jj_entries_loop: for (Iterator it = jj_expentries.iterator(); it.hasNext();) {
        int[] oldentry = (int[])(it.next());
        if (oldentry.length == jj_expentry.length) {
          for (int i = 0; i < jj_expentry.length; i++) {
            if (oldentry[i] != jj_expentry[i]) {
              continue jj_entries_loop;
            }
          }
          jj_expentries.add(jj_expentry);
          break jj_entries_loop;
        }
      }
      if (pos != 0) jj_lasttokens[(jj_endpos = pos) - 1] = kind;
    }
  }

  /** Generate ParseException. */
  static public ParseException generateParseException() {
    jj_expentries.clear();
    boolean[] la1tokens = new boolean[26];
    if (jj_kind >= 0) {
      la1tokens[jj_kind] = true;
      jj_kind = -1;
    }
    for (int i = 0; i < 8; i++) {
      if (jj_la1[i] == jj_gen) {
        for (int j = 0; j < 32; j++) {
          if ((jj_la1_0[i] & (1<<j)) != 0) {
            la1tokens[j] = true;
          }
        }
      }
    }
    for (int i = 0; i < 26; i++) {
      if (la1tokens[i]) {
        jj_expentry = new int[1];
        jj_expentry[0] = i;
        jj_expentries.add(jj_expentry);
      }
    }
    jj_endpos = 0;
    jj_rescan_token();
    jj_add_error_token(0, 0);
    int[][] exptokseq = new int[jj_expentries.size()][];
    for (int i = 0; i < jj_expentries.size(); i++) {
      exptokseq[i] = (int[])jj_expentries.get(i);
    }
    return new ParseException(token, exptokseq, tokenImage);
  }

  /** Enable tracing. */
  static final public void enable_tracing() {
  }

  /** Disable tracing. */
  static final public void disable_tracing() {
  }

  static private void jj_rescan_token() {
    jj_rescan = true;
    for (int i = 0; i < 4; i++) {
    try {
      JJCalls p = jj_2_rtns[i];
      do {
        if (p.gen > jj_gen) {
          jj_la = p.arg; jj_lastpos = jj_scanpos = p.first;
          switch (i) {
            case 0: jj_3_1(); break;
            case 1: jj_3_2(); break;
            case 2: jj_3_3(); break;
            case 3: jj_3_4(); break;
          }
        }
        p = p.next;
      } while (p != null);
      } catch(LookaheadSuccess ls) { }
    }
    jj_rescan = false;
  }

  static private void jj_save(int index, int xla) {
    JJCalls p = jj_2_rtns[index];
    while (p.gen > jj_gen) {
      if (p.next == null) { p = p.next = new JJCalls(); break; }
      p = p.next;
    }
    p.gen = jj_gen + xla - jj_la; p.first = token; p.arg = xla;
  }

  static final class JJCalls {
    int gen;
    Token first;
    int arg;
    JJCalls next;
  }

}
